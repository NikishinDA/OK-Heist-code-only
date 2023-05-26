using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorWaypointAddon : MonoBehaviour
{
    private TimedWaypointController _waypointController;
    [SerializeField] private bool depart;

    private void Awake()
    {
        _waypointController = GetComponent<TimedWaypointController>();
        if (depart)
            _waypointController.TimerFinished += WaypointControllerOnTimerFinished;
        else
            _waypointController.TimerFinished += WaypointControllerOnAgentOnPoint;
    }

    private void WaypointControllerOnAgentOnPoint()
    {
        var evt = GameEventsHandler.TutorialGuardEvent;
        evt.Turn = false;
        EventManager.Broadcast(evt);
    }

    private void WaypointControllerOnTimerFinished()
    {
        var evt = GameEventsHandler.TutorialGuardEvent;
        evt.Turn = true;
        EventManager.Broadcast(evt);
    }
}
