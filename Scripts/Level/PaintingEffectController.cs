using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingEffectController : StealableEffectController
{
    [SerializeField] private ParticleSystem[] effects;
    public override void ActivateEffect()
    {
        foreach (var effect in effects)
        {
            effect.Play();
        }
    }
}
