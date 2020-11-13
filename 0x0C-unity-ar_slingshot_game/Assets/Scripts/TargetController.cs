using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class TargetController : MonoBehaviour
{
    ARPlane plane;
    NavMeshAgent agent;
    public float speed = 0.2f;
    float walkRadius = 0.6f;
    Vector2 dir;
    Vector3 target;


    // void Start()
    // {
    //     plane = GetComponentInParent<ARPlane>();
    //     transform.up = plane.normal;
    //     InvokeRepeating("ChangePosition", 1.0f, 4.0f);
    // }

    // void ChangePosition()
    //  {
    //     dir = Random.insideUnitCircle;
    //     target = transform.localPosition + new Vector3(dir.x, 0.0f, dir.y) * 0.4f;
    //  }

    // void Update()
    // {
    //     if (Vector3.Distance(transform.localPosition, target) > 0.1f)
    //     {
    //         float step = speed * Time.deltaTime;
    //         transform.up = plane.normal;
    //         transform.localPosition += new Vector3(dir.x, 0.0f, dir.y) * step;
    //     }
    // }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("NavMeshMove", Random.Range(2.5f, 3.5f));
    }

    void NavMeshMove()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1)) {
            finalPosition = hit.position;            
            agent.SetDestination(finalPosition);
        }
        Invoke("NavMeshMove", Random.Range(2.5f, 3.5f));
    }
}
