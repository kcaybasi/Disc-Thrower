using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController _playerController;
    DiscThrower _discThrower;
    Animator _animator;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _discThrower = GetComponent<DiscThrower>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        CGameManager.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted()
    {
        _playerController.enabled = true;
        _discThrower.enabled = true;
        _animator.SetBool("IsGameStarted",true);
    }
}
