using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private CoinCounterUI coinCounter;
    [SerializeField] private GameObject[] lifeHearts;
    [SerializeField] GameObject explosion;

    [Header("Player Stats")]
    public int score = 0;
    public int lives = 3;
    private GameObject player;
    private CameraShake cameraShake;
    private bool isCooldown = false;


    [Header("Box break mechanics")]
    private bool downThrust;

    [Header("Freeze Mechanics")]
    private PlayerCollision playerCollision;
    [SerializeField] private Image FreezeIndicator;

    [Header("Timer Mechanics")]
    private Timer timer;

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
        score = GameData.Instance.levelScores[GameData.Instance.currentLevelIndex];
        UpdateHUD();
        FreezeIndicator.enabled = false;
        playerCollision = FindFirstObjectByType<PlayerCollision>();
        playerCollision.OnFreeze.AddListener(ToggleFreeze);
        player = GameObject.FindWithTag("Player");
        cameraShake = FindFirstObjectByType<CameraShake>();
        timer = FindFirstObjectByType<Timer>();
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
        GameData.Instance.levelScores[GameData.Instance.currentLevelIndex] = score;
        UpdateScoreUI();

    }

    public void IncrementLives()
    {
        lives++;
        UpdateHearts();
    }
    public void LoseLife()
    {
        if (isCooldown || lives <= 0) return;

        lives--;
        UpdateHearts();
        StartCoroutine(LoseLifeCooldown());
        cameraShake.StartShake();

        if (lives <= 0)
        {
            Time.timeScale = 1f; 
            GameData.Instance.levelScores[GameData.Instance.currentLevelIndex] = score;
            StartCoroutine(GameOverTransition());
            Destroy(player);
            GameObject effect = Instantiate(explosion, player.transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);
        }

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
        if (lifeHearts == null || lifeHearts.Length == 0) return;

        for (int i = 0; i < lifeHearts.Length; i++)
        {
            lifeHearts[i].SetActive(i < lives);
        }
    }

    private IEnumerator LoseLifeCooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(1f);
        isCooldown = false;
    }

    private IEnumerator GameOverTransition() {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
