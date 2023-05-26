using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    [SerializeField] private Image back;
    [SerializeField] private Sprite[] backSprites;
    [SerializeField] private Vector2[] firstSectors;
    [SerializeField] private Vector2[] secondSectors;
    [SerializeField] private Vector2[] thirdSectors;

    [SerializeField] private Transform arrowTransform;

    private int _stage;
    private Vector2[][] _sectors;
    private void Awake()
    {
        _sectors = new[] {firstSectors, secondSectors, thirdSectors};
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var sector in _sectors[_stage])
            {
                float correctedAngle = arrowTransform.eulerAngles.z - 180;
                correctedAngle = correctedAngle - 180 * Mathf.Sign(correctedAngle);
                if (correctedAngle < sector.x && correctedAngle > sector.y)
                {
                    NextStage();
                    return;
                }
            }
        }
    }

    private void NextStage()
    {
        _stage++;
        if (_stage > 2)
        {
            var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = true;
            EventManager.Broadcast(evt);
            return;
        }

        back.sprite = backSprites[_stage];
    }
}

