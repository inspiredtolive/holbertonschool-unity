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
    /// <summary>
    /// The target to hit.
    /// </summary>
    public GameObject m_Target;
    /// <summary>
    /// A button to replay with the same plane.
    /// </summary>
    public GameObject PlayAgainBtn;
    int remainingAmmo = 6;
    float distance = 0.25f;
    enum status 
    {
        Loaded,
        Firing,
        Fired,
        GameOver
    }
    status mode = status.Loaded;
    SphereCollider coll;
    Rigidbody rb;
    Vector2 initalPos;
    Vector2 releasePos;
    ARPlane plane;
    List<GameObject> targets = new List<GameObject>();
    LineRenderer[] trajectoryLines;

    void Start()
    {
        coll = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        trajectoryLines = GetComponentsInChildren<LineRenderer>();
        footer.SetActive(true);
        plane = GameObject.FindWithTag("Plane").GetComponent<ARPlane>();
        restrictRBProps();
        targets.Add(Instantiate(m_Target, plane.center + new Vector3(0.0f, 0.16f, 0.0f), Quaternion.identity));
        targets.Add(Instantiate(m_Target, plane.center + new Vector3(0.0f, 0.16f, 0.0f), Quaternion.identity));
        targets.Add(Instantiate(m_Target, plane.center + new Vector3(0.0f, 0.16f, 0.0f), Quaternion.identity));
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
            if (remainingAmmo > 0 && targets.Count != 0)
            {
                remainingAmmoUI[--remainingAmmo].SetActive(false);
                restrictRBProps();
                mode = status.Loaded;
            }
            else
            {
                mode = status.GameOver;
                PlayAgainBtn.SetActive(true);
            }
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            targets.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            score += 10;
            scoreUI.text = score.ToString();
            if (targets.Count == 0 || remainingAmmo == 0)
            {
                mode = status.GameOver;
                PlayAgainBtn.SetActive(true);
            }
            else
            {
                remainingAmmoUI[--remainingAmmo].SetActive(false);
                restrictRBProps();
                mode = status.Loaded;
            }
        }
    }

    public void PlayAgain()
    {
        PlayAgainBtn.SetActive(false);
        remainingAmmo = 6;
        foreach (GameObject obj in remainingAmmoUI)
        {
            obj.SetActive(true);
        }
        mode = status.Loaded;
        targets.Add(Instantiate(m_Target, plane.center + new Vector3(0.0f, 0.16f, 0.0f), Quaternion.identity));
        targets.Add(Instantiate(m_Target, plane.center + new Vector3(0.0f, 0.16f, 0.0f), Quaternion.identity));
        targets.Add(Instantiate(m_Target, plane.center + new Vector3(0.0f, 0.16f, 0.0f), Quaternion.identity));
        score = 0;
        scoreUI.text = "0";
        restrictRBProps();
    }

    Vector3 CalculateVelocity()
    {
        Vector3 dir = new Vector3(initalPos.x - releasePos.x, initalPos.y - releasePos.y, 0).normalized;
        dir = Quaternion.AngleAxis(30, cam.transform.right) * dir;
        return dir * Vector2.Distance(initalPos, releasePos) / Screen.height * 8;
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
                        foreach (LineRenderer lr in trajectoryLines)
                            lr.enabled = true;
                    }
                    break;

                case (TouchPhase.Moved):
                    if (mode == status.Firing)
                    {
                        rb.MovePosition(ray.GetPoint(distance));
                        releasePos = touch.position;
                        Vector3 vi = CalculateVelocity();
                        int i = 0;
                        foreach (LineRenderer lr in trajectoryLines)
                        {
                            for (int j = 0; j < lr.positionCount; j++, i++)
                            {
                                float t = i * 0.06f;
                                lr.SetPosition(j, vi * t + 0.5f * Physics.gravity * Mathf.Pow(t, 2) + transform.position);
                            }
                        }
                    }
                    break;
                
                case (TouchPhase.Ended):
                    if (mode == status.Firing)
                    {
                        releasePos = touch.position;
                        rb.AddForce(CalculateVelocity(), ForceMode.VelocityChange);
                        mode = status.Fired;
                        foreach (LineRenderer lr in trajectoryLines)
                            lr.enabled = false;
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
                else
                {
                    mode = status.GameOver;
                    PlayAgainBtn.SetActive(true);
                }
            }
        }
    }
}
