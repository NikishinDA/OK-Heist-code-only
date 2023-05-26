using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimationController : MonoBehaviour
{
      private Animator _animator;

      private void Awake()
      {
            _animator = GetComponent<Animator>();
            EventManager.AddListener<GameStartEvent>(OnGameStart);
      }

      private void OnDestroy()
      {
            EventManager.RemoveListener<GameStartEvent>(OnGameStart);
      }

      private void OnGameStart(GameStartEvent obj)
      {
            _animator.SetBool("OpenDoors", true);
      }
}
