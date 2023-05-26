using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHitbox : MonoBehaviour
{
    public event Action<Vector3> TargetIntercepted; 
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ThiefHurtbox>().GuardInteraction();
        TargetIntercepted?.Invoke((other.transform.position - transform.position).normalized);
        other.enabled = false;
    }
}
