// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsHandler
{
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly FinisherStartEvent FinisherStartEvent = new FinisherStartEvent();
    public static readonly PlayerFinishLevelEvent PlayerFinishLevelEvent = new PlayerFinishLevelEvent();
    public static readonly TutorialShowEvent TutorialShowEvent = new TutorialShowEvent();
    public static readonly TutorialToggleEvent TutorialToggleEvent = new TutorialToggleEvent();
    public static readonly AmbianceChangeEvent AmbianceChangeEvent = new AmbianceChangeEvent();
    public static readonly PlayerModelChangeEvent PlayerModelChangeEvent = new PlayerModelChangeEvent();
    public static readonly DebugCallEvent DebugCallEvent = new DebugCallEvent();
    public static readonly MapToggleEvent MapToggleEvent = new MapToggleEvent();
    public static readonly MapPlanningHideEvent MapPlanningHideEvent = new MapPlanningHideEvent();
    public static readonly TutorialGuardEvent TutorialGuardEvent = new TutorialGuardEvent();
}

public class GameEvent {}

public class GameStartEvent : GameEvent
{
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class MoneyCollectEvent : GameEvent
{
    
}

public class MapToggleEvent : GameEvent
{
    public bool Toggle;
    public List<WaypointController> SelectedWaypoints;
}
public class FinisherStartEvent : GameEvent
{
    
}

public class  PlayerFinishLevelEvent : GameEvent{}

public class TutorialShowEvent : GameEvent
{
}

public class TutorialToggleEvent : GameEvent
{
    public bool Toggle;
}


public class AmbianceChangeEvent : GameEvent
{
    public int Number;
}
public class PlayerModelChangeEvent : GameEvent
{
    public bool Bin;
}
public class DebugCallEvent : GameEvent
{
    public float Speed;
    public float Strafe;
}

public class MapPlanningHideEvent : GameEvent
{
    public bool IsHide;
}

public class TutorialGuardEvent : GameEvent
{
    public bool Turn;
}


