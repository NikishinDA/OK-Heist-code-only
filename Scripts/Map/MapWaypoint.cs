using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapWaypoint : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private Image _image;
    [SerializeField] private WaypointController associatedWaypoint;
    public WaypointController AssociatedWaypoint => associatedWaypoint;

    public event Action<MapWaypoint, Vector2> PointSelected;
    public event Action<MapWaypoint, Vector2> PointClicked;
    public event Action<MapWaypoint, Vector2> PointUnclicked;
    public event Action<MapWaypoint, Vector2> PointDeselected;

    private bool _isSelected;

    private Animator _animator;
    public MapLineController SavedLine
    {
        get;
        set;
    }
    private void Awake()
    {
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
    }
    
    public void ResetSelection()
    {
        _image.color = Color.green;
        _isSelected = false;
    }

    public void SelectPoint()
    {
        _image.color = Color.red;
        _isSelected = true;
        _animator.SetTrigger("Select");
        Taptic.Medium();
    }
    public void ClearLine()
    {
        if (SavedLine)
        {
            Destroy(SavedLine.gameObject);
            SavedLine = null;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (_isSelected)
            {
                PointDeselected?.Invoke(this, transform.localPosition);
            }
            else
            {
                PointSelected?.Invoke(this, transform.localPosition);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isSelected) return;
        _image.color = Color.red;
        PointClicked?.Invoke(this, transform.localPosition);
        _isSelected = true;
    }
}
