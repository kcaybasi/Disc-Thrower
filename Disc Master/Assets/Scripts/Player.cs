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
    ParticleSystem _crashParticles;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _discThrower = GetComponent<DiscThrower>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _crashParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        CGameManager.OnGameStarted += OnGameStarted;
        CGameManager.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        _playerController.enabled = false;
        _discThrower.enabled = false;
        _crashParticles.Play();
        _animator.SetBool("Dead", true);
    }

    private void OnGameStarted()
    {
        _playerController.enabled = true;
        _discThrower.enabled = true;
        _animator.SetBool("IsGameStarted",true);
    }

    private void OnDestroy()
    {
        CGameManager.OnGameStarted -= OnGameStarted;
        CGameManager.OnGameOver -= OnGameOver;
    }
}
