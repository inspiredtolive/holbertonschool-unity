using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
}
