using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class NavMeshBuilder : MonoBehaviour
{
    NavMeshSurface surface;
    ARPlane plane;

    // Start is called before the first frame update
    void Start()
    {
        plane = GetComponent<ARPlane>();
        surface = GetComponent<NavMeshSurface>();
    }

    public void Select()
    {
        surface.BuildNavMesh();
        plane.boundaryChanged += BuildNavMesh;
    }

    void BuildNavMesh(ARPlaneBoundaryChangedEventArgs e)
    {
        surface.BuildNavMesh();
    }
    
}
