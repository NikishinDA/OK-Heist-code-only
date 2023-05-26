using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] private GameObject overlookCamera;
    [SerializeField] private GameObject actionCamera;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);

    }

    private void OnGameStart(GameStartEvent obj)
    {
        overlookCamera.SetActive(false);
        actionCamera.SetActive(true);
    }
}
