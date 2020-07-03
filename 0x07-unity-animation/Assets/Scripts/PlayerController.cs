using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides player movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Transform cam;
    Animator animator;
    CharacterController controller;
    Vector3 spawnPosition;
    float speed = 10f;
    float jumpHeight = 2f;
    float gravity = -9.81f;
    Vector3 velocity;

    /// <summary>
    /// Initializes components and spawn position.
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        spawnPosition = transform.position;
        spawnPosition.y += 20;
    }

    /// <summary>
    /// Computes player movement every frame.
    /// </summary>
    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.01f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0f, z).normalized;
        Vector3 force = Vector3.zero;


        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            force =  moveDir * speed * Time.deltaTime;
            animator.SetBool("isRunning", true);
            transform.LookAt(transform.position + moveDir);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime + force);
        if (transform.position.y < -20)
        {
            transform.position = spawnPosition;
        }
    }
}
