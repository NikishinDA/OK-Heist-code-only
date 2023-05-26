using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealableController : MonoBehaviour
{
    [SerializeField] private TimedWaypointController timedWaypointController;
    [SerializeField] private StealableEffectController effectController;

    private void Awake()
    {
        timedWaypointController.AgentOnPoint += TimedWaypointControllerOnAgentOnPoint;
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin)
        {
            effectController.ActivateEffect();
        }
    }

    private void TimedWaypointControllerOnAgentOnPoint(WaypointType type)
    {
        EventManager.Broadcast(GameEventsHandler.FinisherStartEvent);
    }
}
