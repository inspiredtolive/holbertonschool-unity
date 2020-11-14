using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    /// <summary>
    /// The shoot audio clip.
    /// </summary>
    public AudioClip shoot;
    /// <summary>
    /// The hit audio clip.
    /// </summary>
    public AudioClip hit;
    /// <summary>
    /// The miss audio clip.
    /// </summary>
    public AudioClip miss;
    /// <summary>
    /// The start audio clip.
    /// </summary>
    public AudioClip start;
    /// <summary>
    /// The win audio clip.
    /// </summary>
    public AudioClip win;
    /// <summary>
    /// The game over audio clip.
    /// </summary>
    public AudioClip gameOver;
    /// <summary>
    /// The button audio clip.
    /// </summary>
    public AudioClip button;
    AudioSource source;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playShootSound()
    {
        source.clip = shoot;
        source.Play();
    }

    public void playHitSound()
    {
        source.clip = hit;
        source.Play();
    }
    public void playMissSound()
    {
        source.clip = miss;
        source.Play();
    }
    public void playStartSound()
    {
        source.clip = start;
        source.Play();
    }
    public void playWinSound()
    {
        source.clip = win;
        source.Play();
    }
    public void playGameOverSound()
    {
        source.clip = gameOver;
        source.Play();
    }
    public void playButtonSound()
    {
        source.clip = button;
        source.Play();
    }
}
