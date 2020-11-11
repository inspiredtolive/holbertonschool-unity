using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Manages plane interactions and UI elements.
/// </summary>
public class ARSurfaceManager : MonoBehaviour
{
    /// <summary>
    /// Provides plane detection.
    /// </summary>
    public ARPlaneManager m_ARPlaneManager;
    /// <summary>
    /// Provides plane selection.
    /// </summary>
    public ARRaycastManager m_RaycastManager;
    /// <summary>
    /// A UI Panel indicating that plane detection is on.
    /// </summary>
    public GameObject m_searchPanel;
    /// <summary>
    /// A UI Panel intructing the user to select a plane.
    /// </summary>
    public GameObject m_instructionPanel;
    /// <summary>
    /// A UI Button to start the game.
    /// </summary>
    public GameObject m_startButton;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    bool m_isPlanesFound = false;
    
    
    void Awake()
    {
        m_ARPlaneManager.planesChanged += OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs e)
    {
        if (!m_isPlanesFound && m_ARPlaneManager.trackables.count > 0)
        {
            OnPlanesFound();
        }
        else if (m_isPlanesFound && m_ARPlaneManager.trackables.count == 0)
        {
            OnPlanesExited();
        }
    }

    void OnPlanesFound()
    {
        m_isPlanesFound = true;
        m_searchPanel.SetActive(false);
        m_instructionPanel.SetActive(true);
    }

    void OnPlanesExited()
    {
        m_isPlanesFound = false;
        m_searchPanel.SetActive(true);
        m_instructionPanel.SetActive(false);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (m_ARPlaneManager.enabled)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                m_ARPlaneManager.enabled = false;
                foreach (ARPlane plane in m_ARPlaneManager.trackables)
                {
                    if (plane.trackableId != s_Hits[0].trackableId)
                        plane.gameObject.SetActive(false);
                }
                m_instructionPanel.SetActive(false);
                m_startButton.SetActive(true);
            }
        }
    }
}