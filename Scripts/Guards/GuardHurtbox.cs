using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHurtbox : MonoBehaviour
{
    public event Action<Vector3> NoiseHeard;

    public void Distract(Vector3 distractionPos)
    {
        NoiseHeard?.Invoke(distractionPos);
    }
}
