using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimationController : MonoBehaviour
{
    
    [SerializeField] private GuardAgentController agentController;
    [SerializeField] private GuardHitbox hitbox;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        agentController.AgentSetActive += AgentActivated;
        agentController.GuardAlert += AgentControllerOnGuardAlert;
        hitbox.TargetIntercepted += HitboxOnTargetIntercepted;
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
            _animator.SetTrigger("Defeat");
        }
    }

    private void HitboxOnTargetIntercepted(Vector3 obj)
    {
        _animator.SetTrigger("Beat");
    }

    private void AgentControllerOnGuardAlert(bool obj)
    {
        _animator.SetBool("Run", obj);
    }

    private void AgentActivated(bool obj)
    {
        _animator.SetBool("Move", obj);
    }
}
