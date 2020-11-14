using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loads saved high scores.
/// </summary>
public class LoadHighScore : MonoBehaviour
{
    /// <summary>
    /// An list of high score texts.
    /// </summary>
    public List<Text> highScoreTexts;
    string[] scoreNames = { "FirstScore", "SecondScore", "ThirdScore" };

    void Start()
    {
        for (int i = 0; i < scoreNames.Length; i++)
        {
            if (PlayerPrefs.HasKey(scoreNames[i]))
            {
                highScoreTexts[i].text = PlayerPrefs.GetInt(scoreNames[i]).ToString();
            }
        }
    }

}
