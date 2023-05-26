using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonCLick);
    }

    private void OnStartButtonCLick()
    {
        var evt = GameEventsHandler.MapToggleEvent;
        evt.Toggle = true;
        EventManager.Broadcast(evt);
    }
}
