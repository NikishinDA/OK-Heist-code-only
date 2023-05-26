using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefController : MonoBehaviour
{
    [SerializeField] private ThiefAgentController agentController;
    [SerializeField] private ThiefHurtbox hurtbox;

    private void Awake()
    {
        hurtbox.GetCaught += HurtboxOnGetCaught;
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<MapToggleEvent>(OnMapToggle);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<MapToggleEvent>(OnMapToggle);
    }

    private void OnMapToggle(MapToggleEvent obj)
    {
        if (!obj.Toggle)
            agentController.SetWaypoints(obj.SelectedWaypoints);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        agentController.StartAgent();
    }

    private void HurtboxOnGetCaught()
    {
        agentController.StopAgent();
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = false;
        EventManager.Broadcast(GameEventsHandler.GameOverEvent);
        Taptic.Failure();
    }

    public void SetAgentWaypoints(List<WaypointController> waypoints)
    {
        agentController.SetWaypoints(waypoints);
    }
}
