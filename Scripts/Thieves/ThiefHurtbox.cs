using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefHurtbox : MonoBehaviour
{
    public event Action GetCaught;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void GuardInteraction()
    {
        GetCaught?.Invoke();
    }
    
}
