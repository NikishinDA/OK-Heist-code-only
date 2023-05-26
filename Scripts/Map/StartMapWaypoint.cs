using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMapWaypoint : MonoBehaviour, IPointerDownHandler
{
    public event Action<StartMapWaypoint, Vector2> StartWaypointClicked;


    public void OnPointerDown(PointerEventData eventData)
    {
        StartWaypointClicked?.Invoke(this, transform.localPosition);
    }

}
