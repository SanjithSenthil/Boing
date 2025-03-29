using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("FreezeMechanics")]
    private PlayerCollision playerCollision;
    [SerializeField] private Image FreezeIndicator;

    [Header("Timer Mechanics")]
    [SerializeField] private Timer timer;

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
        FreezeIndicator.enabled = false;
        playerCollision = FindFirstObjectByType<PlayerCollision>();
        playerCollision.OnFreeze.AddListener(ToggleFreeze);
        player = GameObject.FindWithTag("Player");
        timer.ActivateTimer();
    }

    public void ToggleFreeze()
    {
        StartCoroutine(TurnOffFreeze(5f));
    }

    public IEnumerator TurnOffFreeze(float seconds)
    {
        FreezeIndicator.enabled = true;
        yield return new WaitForSeconds(seconds);
        FreezeIndicator.enabled = false;
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

    public void StopGame()
    {
        Debug.Log("Game Over");
        timer.gameObject.SetActive(false);
    }
}
