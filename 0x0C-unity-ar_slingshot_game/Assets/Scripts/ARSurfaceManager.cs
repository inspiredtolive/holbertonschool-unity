using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Manages plane interactions and UI elements.
/// </summary>
public class ARSurfaceManager : MonoBehaviour
{
    public ARPlaneManager m_ARPlaneManager;
    public GameObject m_searchPanel;
    public GameObject m_instructionPanel;
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
}