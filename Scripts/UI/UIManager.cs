using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject preActionScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject mapScreen;
    [SerializeField] private GameObject minigameScreen;

    private void Awake()
    {
        EventManager.AddListener<MapToggleEvent>(OnMapToggle);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MapToggleEvent>(OnMapToggle);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);

    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        minigameScreen.SetActive(true);
    }

    private void Start()
    {
        startScreen.SetActive(true);
    }

    private void OnGameOver(GameOverEvent obj)
    {        
        minigameScreen.SetActive(false);

        if (obj.IsWin)
        {
            StartCoroutine(Timer(3f, winScreen));
            
            //winScreen.SetActive(true);
        }
        else
        {
            StartCoroutine(Timer(3f, loseScreen));

            //loseScreen.SetActive(true);
        }
        
    }

    private void OnMapToggle(MapToggleEvent obj)
    {
        if (obj.Toggle)
        {
            startScreen.SetActive(false);
            mapScreen.SetActive(true);
        }
        else
        {
            mapScreen.SetActive(false);
            preActionScreen.SetActive(true);
        }
    }

    private IEnumerator Timer(float time, GameObject screen)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        screen.SetActive(true);
    }
}
 