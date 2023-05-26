using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorCanvasAddon : MonoBehaviour
{
    [SerializeField] private GameObject finger;
    private void Awake()
    {
        EventManager.AddListener<TutorialGuardEvent>(OnTutorEvent);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<TutorialGuardEvent>(OnTutorEvent);
    }

    private void OnTutorEvent(TutorialGuardEvent obj)
    {
        finger.SetActive(obj.Turn);
    }
}
