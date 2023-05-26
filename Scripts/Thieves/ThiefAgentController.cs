using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ThiefAgentController : AgentController
{
   
    private bool _isActive;
    public event Action<TimedWaypointController> NewPoint;

    protected override void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_isActive)
            CheckDistance();
    }

    public void StartAgent()
    {
        WaypointNum = -1;
        SetNextDestination();
        _isActive = true;
        Activate(true);
    }


    public override void SetNextDestination()
    {
        WaypointNum++;
        if (WaypointNum >= destinations.Length)
        {
            return;
        }
        CurrentWaypoint = destinations[WaypointNum];
        if (CurrentWaypoint is TimedWaypointController wp)
        {
            NewPoint?.Invoke(wp);
        }
        NavMeshAgent.destination = CurrentWaypoint.Point;
        Activate(true);
    }

    public void SetWaypoints(List<WaypointController> waypoints)
    {
        destinations = waypoints.ToArray();
    }
    
    public override void StopAgent()
    {
        base.StopAgent();
        if (CurrentWaypoint is TimedWaypointController obj)
        {
            obj.CancelCurrentOrder();
        }
    }
}
