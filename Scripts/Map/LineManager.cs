using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineManager : MonoBehaviour/*, IBeginDragHandler, IEndDragHandler, IDragHandler*/
{
    [SerializeField] private MapLineController linePrefab;
    private MapLineController _currentLine;
    private PointerEventData _pointerEventData;
    [SerializeField] private Canvas currentCanvas;

    public MapLineController CurrentLine
    {
        get => _currentLine;
        set => _currentLine = value;
    }
    
    public void ResetDraw(Vector2 endPos)
    {
        if (_currentLine)
            _currentLine.SetEndPosition(endPos, false);
        _currentLine = Instantiate(linePrefab, transform);
        _currentLine.currentCanvas = currentCanvas;
        _currentLine.StartDrawing(endPos, false);
        _currentLine.SetEndPosition(endPos, false);
    }

    public void StartDraw(Vector2 startPos)
    {
        _currentLine = Instantiate(linePrefab, transform);
        _currentLine.currentCanvas = currentCanvas;
        _currentLine.StartDrawing(startPos, false);
        _currentLine.SetEndPosition(startPos, false);
    }
    /*public void OnBeginDrag(PointerEventData eventData)
    {
        _currentLine = Instantiate(linePrefab, transform);
        _currentLine.currentCanvas = currentCanvas;
        _isDrawing = true;
        _currentLine.StartDrawing(eventData.position);
    }*/

    public void OnEndDrag(PointerEventData eventData)
    {
        if ( _currentLine) 
            Destroy(_currentLine.gameObject);
        _currentLine = null;
    }

    public void OnDrag(PointerEventData eventData)
    { 
        if ( _currentLine)
            _currentLine.SetEndPosition(eventData.position);
    }

    public void SwitchLineColor(bool isBlocked)
    {
        _currentLine.ChangeColor(isBlocked);
    }
}
