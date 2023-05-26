using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnimationController : MonoBehaviour
{
    [SerializeField] private ThiefAgentController agentController;
    private Animator _animator;

    public void Initialize(ThiefAgentController agentController)
    {
        this.agentController = agentController;
        agentController.AgentSetActive += AgentActivated;
        agentController.NewPoint += AgentControllerOnNewPoint;
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
            _animator.SetTrigger("Win");
        }
        else
        {
            _animator.SetTrigger("Defeat");
        }
    }

    private void AgentControllerOnNewPoint(TimedWaypointController obj)
    {
        obj.AgentOnPoint += ObjOnAgentOnPoint;
    }

    private void ObjOnAgentOnPoint(WaypointType obj)
    {
        switch (obj)
        {
            case WaypointType.Distraction:
            {
                _animator.SetTrigger("Punch");
                return;
            }
            case WaypointType.Stealable:
            {
                _animator.SetTrigger("PickLock");
                return;
            }
        }
    }

    private void AgentActivated(bool isActive)
    {
        _animator.SetBool("Move", isActive);
    }
    
}
