using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionEffectController : MonoBehaviour
{
    [SerializeField]
    private GameObject originalModel;

    [SerializeField] private Rigidbody[] pieces;

    public void ActivateEffect()
    {
        originalModel.SetActive(false);
        foreach (var piece in pieces)
        {
            piece.gameObject.SetActive(true);
            piece.useGravity = true;
            piece.isKinematic = false;
            piece.AddForce(Random.insideUnitSphere * 5f, ForceMode.Impulse);
        }
        Taptic.Heavy();
    }
}
