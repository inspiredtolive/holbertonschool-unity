using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads a level.
    /// </summary>
    /// <param name="level">The level to load.</param>
    public void LevelSelect(int level)
    {
        if (level == 1)
        {
            SceneManager.LoadSceneAsync("Level01");
        }
        else if (level == 2)
        {
            SceneManager.LoadSceneAsync("Level02");
        }
        else if (level == 3)
        {
            SceneManager.LoadSceneAsync("Level03");
        }
    }

    /// <summary>
    /// Loads the options scene.
    /// </summary>
    public void Options()
    {
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync("Options");
    }
}
