using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Represents a target controller.
/// </summary>
public class TargetController : MonoBehaviour
{
    NavMeshAgent agent;
    float walkRadius = 0.5f;

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
