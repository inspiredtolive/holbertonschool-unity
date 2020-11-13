using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AmmoController : MonoBehaviour
{
    /// <summary>
    /// The AR Camera.
    /// </summary>
    public Camera cam;
    /// <summary>
    /// The Text that displays the score.
    /// </summary>
    public Text scoreUI;
    int score = 0;
    /// <summary>
    /// The UI footer.
    /// </summary>
    public GameObject footer;
    /// <summary>
    /// An array of ammo UIs.
    /// </summary>
    public GameObject[] remainingAmmoUI;
    int remainingAmmo = 6;
    float distance = 0.25f;
    enum status 
    {
        Loaded,
        Firing,
        Fired
    }
    status mode = status.Loaded;
    SphereCollider coll;
    Rigidbody rb;
    Vector2 initalPos;
    Vector2 releasePos;
    ARPlane plane;

    void Start()
    {
        coll = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        footer.SetActive(true);
        plane = GameObject.FindWithTag("Plane").GetComponent<ARPlane>();
        restrictRBProps();
    }

    void restrictRBProps()
    {
        rb.freezeRotation = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            if (remainingAmmo > 0)
            {
                remainingAmmoUI[--remainingAmmo].SetActive(false);
                restrictRBProps();
                mode = status.Loaded;
            }
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            score += 10;
            scoreUI.text = score.ToString();
            if (remainingAmmo > 0)
            {
                remainingAmmoUI[--remainingAmmo].SetActive(false);
                restrictRBProps();
                mode = status.Loaded;
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = cam.ScreenPointToRay(touch.position);
            switch (touch.phase)
            {
                case (TouchPhase.Began):
                    if (mode != status.Loaded)
                        return;
                    RaycastHit hit;
                    if (coll.Raycast(ray, out hit, 1.0f))
                    {
                        initalPos = touch.position;
                        transform.position = ray.GetPoint(distance);
                        mode = status.Firing;
                    }
                    break;

                case (TouchPhase.Moved):
                    if (mode == status.Firing)
                    {
                        rb.MovePosition(ray.GetPoint(distance));
                    }
                    break;
                
                case (TouchPhase.Ended):
                    if (mode == status.Firing)
                    {
                        releasePos = touch.position;
                        Vector3 dir = new Vector3(initalPos.x - releasePos.x, initalPos.y - releasePos.y, 0).normalized;
                        dir = Quaternion.AngleAxis(30, cam.transform.right) * dir;
                        rb.AddForce(dir * Vector2.Distance(initalPos, releasePos) / Screen.height * 8, ForceMode.Impulse);
                        mode = status.Fired;
                        rb.freezeRotation = false;
                        rb.useGravity = true;
                    }
                    break;
            }
        }
        else if (mode == status.Loaded)
        {
            rb.MovePosition(cam.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2)).GetPoint(distance));
        }
        else if (mode == status.Fired)
        {
            if (transform.position.y < plane.center.y)
            {
                if (remainingAmmo > 0)
                {
                    remainingAmmoUI[--remainingAmmo].SetActive(false);
                    restrictRBProps();
                    mode = status.Loaded;
                }
            }
        }
    }
}
