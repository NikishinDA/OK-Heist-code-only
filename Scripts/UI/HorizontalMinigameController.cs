using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HorizontalMinigameController : MonoBehaviour
{
    [SerializeField] private RectTransform arrowTransform;
    [SerializeField] private Animator arrowAnimator;
    [SerializeField] private MinigameStage[] stages;
    private MinigameStage _currentStage;
    private int _stageNum;
    private static readonly int s_reset = Animator.StringToHash("Reset");

    [SerializeField] private Animator[] locksAnimators;
    private void Awake()
    {
        ChangeStage(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentStage.Check(arrowTransform.anchoredPosition.x))
            {
                locksAnimators[_stageNum].SetTrigger("UnLock");
                Taptic.Success();
                if (_stageNum + 1 == stages.Length)
                {
                    var evt = GameEventsHandler.GameOverEvent;
                    evt.IsWin = true;
                    EventManager.Broadcast(evt);
                    return;
                }
                else
                {
                    ChangeStage(_stageNum + 1);
                }
            }
            else
            {
                Taptic.Warning();
            }
            arrowAnimator.SetTrigger(s_reset);
        }
    }

    private void ChangeStage(int num)
    {
        if (_currentStage)
        {
            _currentStage.gameObject.SetActive(false);
        }
        _stageNum = Mathf.Clamp( num, 0 , stages.Length - 1);
        _currentStage = stages[_stageNum];
        _currentStage.gameObject.SetActive(true);
    }
}
