using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private CoinCounterUI coinCounter;
    [SerializeField] private List<GameObject> lifeHearts;

    [Header("Player Stats")]
    public int score = 0;
    public int lives = 3;
    private GameObject player;
    private bool downThrust;
    
    public bool GetDownThrust()
    {
        return downThrust;
    }

    public void SetDownThrust(bool flag)
    {
        downThrust = flag;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateHUD();
        player = GameObject.FindWithTag("Player");
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreUI();
    }

    public void LoseLife()
    {
        if (lives <= 0) return;

        lives--;
        UpdateHearts();

        if (lives <= 0)
        {
            Time.timeScale = 1f; 
            GameData.finalScore = score;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            Destroy(player);
        }

    }

    public void IncrementLives()
    {
        lives++;
        UpdateHearts();
    }

    private void UpdateHUD()
    {
        UpdateScoreUI();
        UpdateHearts();
    }

    private void UpdateScoreUI()
    {
        if (coinCounter != null)
            coinCounter.UpdateScore(score);
    }

    private void UpdateHearts()
    {
        if (lifeHearts == null || lifeHearts.Count == 0) return;

        for (int i = 0; i < lifeHearts.Count; i++)
        {
            lifeHearts[i].SetActive(i < lives);
        }
    }
}
