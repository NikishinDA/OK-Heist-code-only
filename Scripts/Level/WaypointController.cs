using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    
    [SerializeField] protected float actionTime;
    [SerializeField] protected  bool respectRotation;
    public Vector3 Point => transform.position;

    public void SetPosition(Vector3 pos)
    {
        pos.y = 0;
        transform.position = pos;
    }
    public virtual void AgentReached(AgentController agent)
    {
        agent.SetNextDestination();
        if (respectRotation)
        {
            agent.SetAgentRotation(transform.forward, actionTime);
        }
    }
}
