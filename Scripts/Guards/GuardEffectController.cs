using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEffectController : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private ParticleSystem alertEffect;
    [SerializeField] private ParticleSystem investigateEffect;

    [Header("Scripts")]
    [SerializeField] private GuardDetection detection;
    [SerializeField] private GuardHurtbox hurtbox;
    [SerializeField] private GuardAgentController agentController;
    [Header("Cone")]
    [SerializeField] private Renderer viewRenderer;

    [SerializeField] private Color whiteColor;
    [SerializeField] private Color yellowColor;
    [SerializeField] private Color redColor;
    private void Awake()
    {
        detection.TargetSighted += DetectionOnTargetSighted;
        hurtbox.NoiseHeard += HurtboxOnNoiseHeard;
        agentController.GuardAlert += AgentControllerOnGuardAlert;
    }

    private void AgentControllerOnGuardAlert(bool obj)
    {
        if (!obj)
            viewRenderer.material.color = whiteColor;
    }

    private void HurtboxOnNoiseHeard(Vector3 obj)
    {
        viewRenderer.material.color = yellowColor;
        investigateEffect.Play();
    }

    private void DetectionOnTargetSighted(Transform obj)
    {
        viewRenderer.material.color = redColor;
        alertEffect.Play();
        Taptic.Warning();
    }
}
