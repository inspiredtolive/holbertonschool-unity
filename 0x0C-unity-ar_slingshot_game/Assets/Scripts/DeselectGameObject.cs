using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Deselects the current GameObject.
/// </summary>
public class DeselectGameObject : MonoBehaviour
{
    public void Deselect()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
