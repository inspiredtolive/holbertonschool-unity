using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents an options menu.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void Back()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
