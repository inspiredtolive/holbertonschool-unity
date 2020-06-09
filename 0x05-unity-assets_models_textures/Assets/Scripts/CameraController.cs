using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides third-person camera tracking.
/// </summary>
public class CameraController : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target;
    float mouseX, mouseY;

    /// <summary>
    /// Hides cursor and locks cursor to center of view.
    /// </summary>
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Updates the camera position based on mouse input.
    /// </summary>
    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
}
