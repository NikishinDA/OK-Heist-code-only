using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeEffectController : StealableEffectController
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem[] particleSystems;

    public override void ActivateEffect()
    {
        animator.SetTrigger("Open");
        foreach (var system in particleSystems)
        {
            system.Play();
        }
    }
}
