using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardAgentController : AgentController
{
    [SerializeField] private WaypointController chasePoint;
    private int lastDestination = -1;

    public event Action<bool> GuardAlert;
    protected override void Awake()
    {
        base.Awake();
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (CurrentWaypoint is TimedWaypointController wp)
        {
            wp.CancelCurrentOrder();
        }
        StopAgent();
    }

    protected override void Start()
    {
        base.Start();
        WaypointNum = -1;
        SetNextDestination();
        Activate(true);
    }

    private void Update()
    {
        CheckDistance();
    }

    public override void SetNextDestination()
    {
        WaypointNum++;
        if (WaypointNum >= destinations.Length)
        {
            WaypointNum = 0;
        }
        if (WaypointNum == lastDestination)
            return;
        lastDestination = WaypointNum;
        CurrentWaypoint = destinations[WaypointNum];
        NavMeshAgent.destination = CurrentWaypoint.Point;
        Activate(true);
        GuardAlert?.Invoke(false);
    }

    public void SetTargetDestination(Vector3 point)
    {
        if (CurrentWaypoint is TimedWaypointController obj)
        {
            obj.CancelCurrentOrder();
        }

        lastDestination = -1;
        chasePoint.SetPosition(point);
        CurrentWaypoint = chasePoint;
        NavMeshAgent.destination = point;
        Activate(true);
        
        GuardAlert?.Invoke(true);
    }

}
