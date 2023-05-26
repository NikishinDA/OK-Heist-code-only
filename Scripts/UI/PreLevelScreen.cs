using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;
    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        EventManager.Broadcast(GameEventsHandler.GameStartEvent);
        gameObject.SetActive(false);
    }
}
