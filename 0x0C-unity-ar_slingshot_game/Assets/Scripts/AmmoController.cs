using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public Camera cam;
    float distance = 0.5f;
    enum status 
    {
        Loaded,
        Firing,
        Fired
    }
    status mode = status.Loaded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == status.Loaded)
        {
            transform.position = cam.transform.position + cam.transform.forward * distance;
        }
    }
}
