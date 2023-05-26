using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AgentController : MonoBehaviour
{
    [SerializeField] protected WaypointController[] destinations;
    [SerializeField] protected float distanceTolerance = 0.05f;
    [SerializeField] protected float rotationTime = 0.5f;
    protected WaypointController CurrentWaypoint;
    protected int WaypointNum;
    protected NavMeshAgent NavMeshAgent;
    private bool _active;

    public event Action<bool> AgentSetActive;
    protected virtual void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        NavMeshAgent.enabled = true;
    }

    protected void CheckDistance()
    {
        if (_active && ((CurrentWaypoint.Point - transform.position).sqrMagnitude < distanceTolerance)) 
        {
            Activate(false);
            CurrentWaypoint.AgentReached(this);
        }
    }

    protected void Activate(bool isActive)
    {
        _active = isActive;
        AgentSetActive?.Invoke(_active);
    }
    public abstract void SetNextDestination();

    public void SetAgentRotation(Vector3 forward, float timeOut = 1000f)
    {
        StartCoroutine(RotateAgentCor(rotationTime, Quaternion.LookRotation(forward), timeOut));
    }
    private IEnumerator RotateAgentCor(float time, Quaternion lookRotation, float timeOut = 1000f)
    {
        //float to = 0f;
        /*while (NavMeshAgent.hasPath)
        {
            to += Time.deltaTime;
            if (to >= timeOut)
            {
                transform.rotation = lookRotation;
                yield break;
            }

            yield return null;
        }*/
        Quaternion oldRot = transform.rotation;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(oldRot, lookRotation, t/time);
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    public virtual void StopAgent()
    {
        NavMeshAgent.ResetPath();
        NavMeshAgent.isStopped = true;
        Activate(false);
    }
}
