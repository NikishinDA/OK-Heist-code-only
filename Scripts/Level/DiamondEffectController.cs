using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondEffectController : StealableEffectController
{
    [SerializeField] private GameObject glass;
    [SerializeField] private Rigidbody[] shardsRB;
    [SerializeField] private Transform explosionPosition;

    [SerializeField] private ParticleSystem[] effects;
    public override void ActivateEffect()
    {
        glass.SetActive(false);
        foreach (var shard in shardsRB)
        {
            shard.gameObject.SetActive(true);
            shard.useGravity = true;
            shard.isKinematic = false;
            shard.AddExplosionForce(1f, explosionPosition.position, 5f, 0, ForceMode.Impulse);
        }

        foreach (var effect in effects)
        {
            effect.Play();
        }
    }
}
