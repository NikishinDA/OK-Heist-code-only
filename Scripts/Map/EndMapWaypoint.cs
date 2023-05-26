using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndMapWaypoint : MonoBehaviour, IPointerEnterHandler
{
    
    public event Action<EndMapWaypoint, Vector2> EndWaypointSelected;
    

    [SerializeField] private WaypointController associatedWaypoint;
    public WaypointController AssociatedWaypoint => associatedWaypoint;
    public MapLineController SavedLine { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            EndWaypointSelected?.Invoke(this, transform.localPosition);
        }
    }
}
