using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float delay;
    public float timeToReachTarget;
    public bool modifyScale = true;
    public string url;
    Vector3 target;
    Vector3 scale;
    Vector3 startPosition = new Vector3(0f, 0f,0f);
    Vector3 velocity = Vector3.zero;
    float currentTime = 0f;
    bool animate = false;

    void Awake()
    {
        target = transform.localPosition;
        scale = transform.localScale;
        transform.localPosition = startPosition;
        if (modifyScale == false)
        {
            transform.localScale = startPosition;
        }
    }

    public void OnFound()
    {
        Invoke("Animate", delay);
    }

    public void OnLost()
    {
        animate = false;
        currentTime = 0f;
        transform.localPosition = startPosition;
        if (modifyScale == false)
        {
            transform.localScale = startPosition;
        }
    }

    void Animate()
    {
        animate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (animate == true)
        {

            currentTime += Time.deltaTime / timeToReachTarget;
            transform.localPosition = Vector3.Lerp(startPosition, target, currentTime);
            if (modifyScale == false)
            {
                transform.localScale = Vector3.Lerp(startPosition, scale, currentTime);
            }
        }
    }

    void OnMouseDown()
    {
        Application.OpenURL(url);
    }   
}