using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;
    public bool isPaused = false;

    /// <summary>
    /// Checks if player presses the pause button.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Pauses the game and shows the pause menu.
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        PauseCanvas.SetActive(true);
    }

    /// <summary>
    /// Resumes the game and hides the pause menu.
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PauseCanvas.SetActive(false);
    }
}
