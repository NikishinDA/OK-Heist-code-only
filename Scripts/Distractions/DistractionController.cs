using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionController : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TimedWaypointController timedWaypointController;
    [SerializeField] private DistractionEffectController effectController;

    private void Awake()
    {
        timedWaypointController.TimerFinished += TimedWaypointControllerOnTimerFinished;
    }

    private void TimedWaypointControllerOnTimerFinished()
    {
        Collider[] guardColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (var guardCollider in guardColliders)
        {
            guardCollider.GetComponent<GuardHurtbox>().Distract(timedWaypointController.Point);
        }
        effectController.ActivateEffect();
    }
}
