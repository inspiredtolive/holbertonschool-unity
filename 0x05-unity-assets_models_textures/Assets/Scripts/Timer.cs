using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Represents the Timer.
/// </summary>
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float time = 0f;

    /// <summary>
    /// Updates the timer UI.
    /// </summary>
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = string.Format("{0:0}:{1:00}.{2:00}", time / 60, time % 60, time * 100 % 100);
    }
}
