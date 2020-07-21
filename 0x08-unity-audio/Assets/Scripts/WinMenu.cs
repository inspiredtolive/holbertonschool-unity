using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a win menu.
/// </summary>
public class WinMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Loads the next level or the main menu.
    /// </summary>
    public void Next()
    {
        if (SceneManager.GetActiveScene().name == "Level01")
        {
            SceneManager.LoadSceneAsync("Level02");
        }
        else if (SceneManager.GetActiveScene().name == "Level02")
        {
            SceneManager.LoadSceneAsync("Level03");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        Time.timeScale = 1f;
    }
}
