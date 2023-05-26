using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform begin;
    [SerializeField] private Transform end;
    public Canvas currentCanvas;
    private Vector2 _correctedStartPos;
    private Vector2 _endPos;
    private bool _colorChanged;

    public void StartDrawing(Vector2 startPos, bool transformPoint = true)
    {
        if (transformPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, startPos,
                currentCanvas.worldCamera, out _correctedStartPos);
            begin.localPosition = _correctedStartPos;
        }
        else
        {
            begin.localPosition = startPos;
        }
        lineRenderer.SetPosition(0, begin.localPosition);
    }

    public void SetEndPosition(Vector2 pos, bool transformPoint = true)
    {
        if (transformPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, pos,
                currentCanvas.worldCamera, out _endPos);
            end.localPosition = _endPos;
        }
        else
        {
            end.localPosition = pos;
        }

        lineRenderer.SetPosition(1, end.localPosition);
    }

    public void ChangeColor(bool isBlocked)
    {
        if (isBlocked == _colorChanged) return;
        
        if (isBlocked)
        {
            lineRenderer.material.color = Color.red;
            _colorChanged = true;
        }
        else
        {
            lineRenderer.material.color = Color.green;
            _colorChanged = false;   
        }
    }
}
