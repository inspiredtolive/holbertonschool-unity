using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides a timer trigger.
/// </summary>
public class TimerTrigger : MonoBehaviour
{
    public GameObject player;

    /// <summary>
    /// Triggers the timer when the player moves.
    /// </summary>
    /// <param name="other">The collider that exited the trigger area.</param>
    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<Timer>().enabled = true;
    }
}
