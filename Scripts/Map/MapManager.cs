using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapManager : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private MapWaypoint[] mappedWaypoints;
    [SerializeField] private StartMapWaypoint firstStartMapWaypoint;
    [SerializeField] private EndMapWaypoint endMapWaypoint;
    private List<MapWaypoint> _waypointsFirst;
    //[SerializeField] private ThiefController thiefController;
    [SerializeField] private LineManager lineManager;
    private bool _isDrawing;
    private bool _isDraged;
    [Header("Debug")]
    [SerializeField] private Canvas currentCanvas;
    private Vector3 _startRaycastPoint;
    private Vector3 _endRaycastPoint;
    [SerializeField] private LayerMask layerMask;
    private bool _isBlocked;
    [SerializeField] private GameObject vcamera;
    private void Awake()
    {
        _waypointsFirst = new List<MapWaypoint>();
        foreach (var mapWaypoint in mappedWaypoints)
        {
            mapWaypoint.PointSelected += OnPointSelect;
            mapWaypoint.PointDeselected += OnPointDeselect;
            mapWaypoint.PointClicked += OnPointClick;
            mapWaypoint.PointUnclicked += OnPointUnclick;
        }
        firstStartMapWaypoint.StartWaypointClicked += StartMapWaypointOnStartWaypointClicked;
        endMapWaypoint.EndWaypointSelected += EndMapWaypointOnEndWaypointClicked;
    }

    private void EndMapWaypointOnEndWaypointClicked(EndMapWaypoint wpController, Vector2 actionPosition)
    {
        if (!_isDrawing || _isBlocked) return;
        lineManager.ResetDraw(actionPosition);
        var evt = GameEventsHandler.MapToggleEvent;
        evt.SelectedWaypoints = ConfirmSelection();
        evt.Toggle = false;
        EventManager.Broadcast(evt);
        _isDrawing = false;
    }

    private void StartMapWaypointOnStartWaypointClicked(StartMapWaypoint startPointController, Vector2 actionPosition)
    {
        ResetAllPoints();
        _isDrawing = true;
        _startRaycastPoint = startPointController.transform.position;
        lineManager.StartDraw(actionPosition);
    }

    private void OnPointUnclick(MapWaypoint wpController, Vector2 actionPosition)
    {
        int index = _waypointsFirst.IndexOf(wpController);
        lineManager.CurrentLine = wpController.SavedLine;
        for (int i = _waypointsFirst.Count - 1; i > index; i--)
        {
            if (i > index)
                _waypointsFirst[i].ClearLine();
            RemovePointAt(i);
        }
    }

    private void OnPointClick(MapWaypoint wpController, Vector2 actionPosition)
    {
        
        int index = _waypointsFirst.IndexOf(wpController);
        lineManager.CurrentLine = wpController.SavedLine;
        for (int i = _waypointsFirst.Count - 1; i > index; i--)
        {
            if (i > index)
                _waypointsFirst[i].ClearLine();
            RemovePointAt(i);
        }

        _isDrawing = true;
        lineManager.ResetDraw(actionPosition);
        
        _startRaycastPoint = wpController.transform.position;
    }

    private void OnConfirmButtonClick()
    {
        //thiefController.SetAgentWaypoints(ConfirmSelection());
        var evt = GameEventsHandler.MapToggleEvent;
        evt.SelectedWaypoints = ConfirmSelection();
        evt.Toggle = false;
        EventManager.Broadcast(evt);
    }

    private void OnPointSelect(MapWaypoint wpController, Vector2 actionPosition)
    {
        if (!_isDrawing || _isBlocked || !_isDraged) return;
        wpController.SelectPoint();
        _waypointsFirst.Add(wpController);
        wpController.SavedLine = lineManager.CurrentLine;
        //_isDrawing = true;
        lineManager.ResetDraw(actionPosition);
        _startRaycastPoint = wpController.transform.position;
    }

    private void OnPointDeselect(MapWaypoint wpController, Vector2 actionPosition)
    {        
        if (!_isDrawing || !_isDraged) return;

        int index = _waypointsFirst.IndexOf(wpController);
        if (_waypointsFirst.Count - 1 == index)
        {
            if (lineManager.CurrentLine)
                Destroy(lineManager.CurrentLine.gameObject);
            lineManager.CurrentLine = wpController.SavedLine;
            //_waypoints[index].ClearLine();
            RemovePointAt(index);
        }

        if (_waypointsFirst.Count >= 1)
            _startRaycastPoint = _waypointsFirst[_waypointsFirst.Count - 1].transform.position;
        else
            _startRaycastPoint = firstStartMapWaypoint.transform.position;
    }

    private void RemovePointAt(int i)
    {
        _waypointsFirst[i].ResetSelection();
        _waypointsFirst.RemoveAt(i);
    }

    private List<WaypointController> ConfirmSelection()
    {
        List < WaypointController > result = _waypointsFirst.Select(mapWaypoint => mapWaypoint.AssociatedWaypoint).ToList();
        result.Add(endMapWaypoint.AssociatedWaypoint);
        return result;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDrawing)
        {
            bool block = RaycastCheck(eventData);
            lineManager.SwitchLineColor(block);
            //if (!block)
                lineManager.OnDrag(eventData);
        }
    }

    private bool RaycastCheck(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, eventData.position,
            currentCanvas.worldCamera, out _endRaycastPoint);
        RaycastHit hit;
        if (Physics.Linecast(_startRaycastPoint, _endRaycastPoint, out hit, layerMask))
        {
            //Debug.DrawLine(_startRaycastPoint,
           //     _endRaycastPoint , Color.green);
            _isBlocked = true;
            return true;
        }
        else
        {
            _isBlocked = false;
            //Debug.DrawLine(_startRaycastPoint,
            //    _endRaycastPoint , Color.red);
            
            return false;
        }
        //Debug.DrawLine(_startRaycastPoint, _endRaycastPoint, Color.green, 1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDraged = false;
        if (_isDrawing)
        {
            lineManager.OnEndDrag(eventData);
            _isDrawing = false;
        }
    }

    public void ResetAllPoints()
    {
        for (var i = _waypointsFirst.Count - 1; i >= 0; i--)
        {
            _waypointsFirst[i].ClearLine();
            RemovePointAt(i);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDraged = true;
    }
}