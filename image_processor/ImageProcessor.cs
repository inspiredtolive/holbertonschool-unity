using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents an image processor.
/// </summary>
class ImageProcessor
{
    /// <summary>
    /// Creates inverted images.
    /// </summary>
    /// <param name="filenames">The array of image filenames to invert.</param>
    public static void Inverse(string[] filenames)
    {
        ChangeFiles(filenames, "_inverse", (byte[] bytes) => {

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)(255 - bytes[i]);
                
        });
    }

    /// <summary>
    /// Creates grayscale images.
    /// </summary>
    /// <param name="filenames">The array of image filenames to convert to grayscale.</param>
    public static void Grayscale(string[] filenames)
    {
        ChangeFiles(filenames, "_grayscale", (byte[] bytes) => {

            for (int i = 0; i < bytes.Length - 2; i += 3)
            {
                byte avg = (byte)((bytes[i] + bytes[i + 1] + bytes[i + 2]) / 3);

                bytes[i] = avg;
                bytes[i + 1] = avg;
                bytes[i + 2] = avg;
            }

        });
    }

    /// <summary>
    /// Creates black and white images.
    /// </summary>
    /// <param name="filenames">The array of image filenames to convert to black and white.</param>
    /// <param name="threshold">The luminance threshold.</param>
    public static void BlackWhite(string[] filenames, double threshold)
    {
        ChangeFiles(filenames, "_bw", (byte[] bytes) => {

            for (int i = 0; i < bytes.Length - 2; i += 3)
            {
                byte val = 0;

                if ((bytes[i] + bytes[i + 1] + bytes[i + 2]) >= threshold)
                    val = 255;

                bytes[i] = val;
                bytes[i + 1] = val;
                bytes[i + 2] = val;
            }

        });
    }

    /// <summary>
    /// Creates thumbnail images.
    /// </summary>
    /// <param name="filenames">The array of image filenames to create thumbnails from.</param>
    /// <param name="height">The height of the thumbnail (retains aspect ratio).</param>
    public static void Thumbnail(string[] filenames, int height)
    {
        Parallel.ForEach(filenames, (filename) => {
            string name = Path.GetFileNameWithoutExtension(filename);
            string extension = Path.GetExtension(filename);

            Image im = Image.FromFile(filename);
            int width = im.Width / (im.Height / height);
            Image thumb = im.GetThumbnailImage(width, height, ()=>false, IntPtr.Zero);

            thumb.Save($"{name}_th{extension}");
        });
    }

    /// <summary>
    /// Processes files in parallel.
    /// </summary>
    /// <param name="filenames">The array of filenames to process.</param>
    /// <param name="app">The filename suffix before extension.</param>
    /// <param name="f">The processing function.</param>
    public static void ChangeFiles(string[] filenames, string app, Action<byte[]> f)
    {
        Parallel.ForEach(filenames, (filename) => {
            BitmapHelper(f, filename, app);
        });
    }

    /// <summary>
    /// Creates locked bitmaps for images.
    /// </summary>
    /// <param name="f">The processing function.</param>
    /// <param name="filename">The original filename.</param>
    /// <param name="app">The filename suffix before extension.</param>
    public static void BitmapHelper(Action<byte[]> f, string filename, string app)
    {
        string name = Path.GetFileNameWithoutExtension(filename);
        string extension = Path.GetExtension(filename);

        Bitmap bm = new Bitmap(filename);

        Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
        System.Drawing.Imaging.BitmapData bmData =
            bm.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            bm.PixelFormat);

        IntPtr p = bmData.Scan0;

        int bytes = Math.Abs(bmData.Stride) * bm.Height;
        byte[] rgbValues = new byte[bytes];

        System.Runtime.InteropServices.Marshal.Copy(p, rgbValues, 0, bytes);

        f(rgbValues);

        System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, p, bytes);

        bm.UnlockBits(bmData);
        
        bm.Save($"{name}{app}{extension}");
    }
}
