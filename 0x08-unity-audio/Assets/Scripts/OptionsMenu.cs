using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents an options menu.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    public Toggle InvertYToggle;

    void Awake()
    {
        if (PlayerPrefs.GetString("IsInverted") == "True")
        {
            InvertYToggle.isOn = true;
        }
    }

    /// <summary>
    /// Loads the previous scene.
    /// </summary>
    public void Back()
    {
        if (PlayerPrefs.GetString("PreviousScene") == "")
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        else
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString("PreviousScene"));
        }
    }

    /// <summary>
    /// Applys/saves selected options.
    /// </summary>
    public void Apply()
    {
        PlayerPrefs.SetString("IsInverted", InvertYToggle.isOn.ToString());
    }
}
