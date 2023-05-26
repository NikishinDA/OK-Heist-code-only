using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaypointType
{
    Simple,
    Distraction,
    Stealable
}

public class TimedWaypointController : WaypointController
{
    //[SerializeField] private float actionTime;
    ///[SerializeField] private bool respectRotation;
    [SerializeField] private WaypointType type;
    private IEnumerator timerCor;
    public event Action TimerFinished; 
    public event Action<WaypointType> AgentOnPoint;
    public override void AgentReached(AgentController agent)
    {
        AgentOnPoint?.Invoke(type);
        StartCoroutine(timerCor = Timer(actionTime, agent));
        if (respectRotation)
        {
            agent.SetAgentRotation(transform.forward, actionTime);
        }
    }

    private IEnumerator Timer(float time, AgentController agent)
    {
        if (time >= 0f)
            yield return new WaitForSeconds(time);
        else
        {
            yield return null;
        }
        agent.SetNextDestination();
        TimerFinished?.Invoke();
    }

    public void CancelCurrentOrder()
    {
        if (timerCor != null)
        {
            StopCoroutine(timerCor);
            timerCor = null;
        }
    }
}
