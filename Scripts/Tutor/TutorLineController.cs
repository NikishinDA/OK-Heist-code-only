using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorLineController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, startTransform.localPosition);
        _lineRenderer.SetPosition(1, endTransform.parent.localPosition + endTransform.localPosition);
    }
}
