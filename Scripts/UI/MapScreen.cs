using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{
    [SerializeField] private Button mapToggleButton;
    private bool _isHidden = false;
    private void Awake()
    {
        mapToggleButton.onClick.AddListener(OnMapButtonClick);
    }

    private void OnMapButtonClick()
    {
        _isHidden = !_isHidden;
        var evt = GameEventsHandler.MapPlanningHideEvent;
        evt.IsHide = _isHidden;
        EventManager.Broadcast(evt);
        
    }
}
