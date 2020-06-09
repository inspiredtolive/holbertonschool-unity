using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 10f;
    private float jumpHeight = 2f;
    private float gravity = -9.81f;
    private Vector3 velocity;

    /// <summary>
    /// Computes player movement every frame.
    /// </summary>
    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.01f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0f, z).normalized;
        Vector3 force =  dir * speed * Time.deltaTime;

        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime + force);
    }
}
