using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CGameManager : MonoBehaviour
{
    //Create a singleton
    public static CGameManager Instance;
    public static Action OnGameStarted;
    public static Action OnGameOver;
    public static Action OnLevelCompleted;
    private bool _isGameStarted;
    [SerializeField] private ParticleSystem levelCompleteParticles;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject levelCompletedMenu;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI levelText;
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
        
        CreatePrefKeys("Cash",_cash);
        UpdateCashText();
        UpdateLevelText();
    }

    private void Start()
    {
        CashPile.OnCollected += OnCashCollected;
    }

    void CreatePrefKeys(string key,int value)
    {
        if (PlayerPrefs.HasKey("Cash"))
        {
            _cash=PlayerPrefs.GetInt("Cash");
        }
        else
        {
            PlayerPrefs.SetInt("Cash", value);
        }
    }
    
    void UpdateCashText()
    {
        cashText.text = "$  " + _cash;
    }

    void UpdateLevelText()
    {
        levelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex+1);
    }
    
    public void OnCashCollected()
    {
        _cash += 5;
        PlayerPrefs.SetInt("Cash", _cash);
        UpdateCashText();
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
    
    // If player finishes the game, the game ends
    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
        levelCompletedMenu.SetActive(true);
        levelCompleteParticles.Play();
    }
    
    // If player restarts the game, the game starts again
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverMenu.SetActive(false);
        _isGameStarted = false;
        startText.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelCompletedMenu.SetActive(false);
        _isGameStarted = false;
        startText.SetActive(true);
    }

    private void OnDestroy()
    {
        CashPile.OnCollected -= OnCashCollected;
    }
}
