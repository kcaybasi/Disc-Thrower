using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour
{
    //Create a singleton
    public static CGameManager Instance;
    public static Action OnGameStarted;
    public static Action OnGameOver;
    private bool _isGameStarted;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI cashText;
    private int _cash;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void Start()
    {
        CashPile.OnCollected += OnCashCollected;
    }

    private void OnCashCollected()
    {
        _cash += 5;
        cashText.text = "$  " + _cash;
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
    
    // If player dies, the game ends
    public void GameOver()
    {
        OnGameOver?.Invoke();
        gameOverMenu.SetActive(true);
    }
    
    // If player restarts the game, the game starts again
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverMenu.SetActive(false);
        _isGameStarted = false;
        startText.SetActive(true);
    }

    private void OnDestroy()
    {
        CashPile.OnCollected -= OnCashCollected;
    }
}
