using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GuardState
{
    Patrol,
    Chase,
    Stop
}
public class GuardController : MonoBehaviour
{
    [SerializeField] private GuardAgentController agentController;
    [SerializeField] private GuardDetection detection;
    [SerializeField] private GuardHitbox hitbox;
    [SerializeField] private GuardHurtbox hurtbox;
    private GuardState _state = GuardState.Patrol;
    private Transform _lockedTarget;
    //public GuardState State => _state;

    private void Awake()
    {
        detection.TargetSighted += TargetSighted;
        hitbox.TargetIntercepted += HitboxOnTargetIntercepted;
        hurtbox.NoiseHeard += HurtboxOnNoiseHeard;
    }

    private void HurtboxOnNoiseHeard(Vector3 obj)
    {
        
        agentController.SetTargetDestination(obj);
    }

    private void HitboxOnTargetIntercepted(Vector3 forward)
    {
        _state = GuardState.Stop;
        agentController.StopAgent();
        agentController.SetAgentRotation(forward);
    }

    public void TargetSighted(Transform target)
    {
        _state = GuardState.Chase;
        _lockedTarget = target;
        detection.ToggleTrigger(false);
        InvokeRepeating(nameof(ChaseCheck), 0f, 0.1f);
    }

    private void ChaseCheck()
    {
        _lockedTarget = detection.CheckVisualContact(_lockedTarget);
        if (_lockedTarget)
        {
            agentController.SetTargetDestination(_lockedTarget.position);
        }
        else
        {
            CancelInvoke(nameof(ChaseCheck));
            detection.ToggleTrigger(true);
            _state = GuardState.Patrol;
        }
    }

    public void ResetGuard()
    {
        _state = GuardState.Patrol;
    }
}
