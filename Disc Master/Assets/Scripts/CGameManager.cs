using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
    public static Action OnGameStarted;
    private bool _isGameStarted;
    [SerializeField] private GameObject startText;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    // If player touches the screen, the game starts
    private void Update()
    {
        if (_isGameStarted) return;
        if (Input.GetMouseButtonDown(0))
        {
            _isGameStarted = true;
            OnGameStarted?.Invoke();
            startText.SetActive(false);
        }
    }

}
