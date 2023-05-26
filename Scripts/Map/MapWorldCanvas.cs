using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWorldCanvas : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject tutor;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        EventManager.AddListener<MapToggleEvent>(OnMapToggle);
        EventManager.AddListener<MapPlanningHideEvent>(OnMapHide);
        if (tutor)
        {
            tutor.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MapToggleEvent>(OnMapToggle);
        EventManager.RemoveListener<MapPlanningHideEvent>(OnMapHide);
    }

    private void OnMapHide(MapPlanningHideEvent obj)
    {
        _animator.SetBool("Toggle", !obj.IsHide);
    }

    private void OnMapToggle(MapToggleEvent obj)
    {
        _animator.SetBool("Toggle", obj.Toggle);
    }
}
