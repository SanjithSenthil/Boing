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
    [SerializeField] private GameObject[] lifeHearts;
    [SerializeField] GameObject explosion;


    [Header("Player Stats")]
    public int score = 0;
    public int lives = 3;
    private GameObject player;
    private bool downThrust;

    [Header("FreezeMechanics")]
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
    
    private CameraShake cameraShake;
    private bool isCooldown = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        score = GameData.Instance.levelScores[GameData.Instance.currentLevelIndex];
        cameraShake = FindFirstObjectByType<CameraShake>();
        UpdateHUD();
        FreezeIndicator.enabled = false;
        playerCollision = FindFirstObjectByType<PlayerCollision>();
        playerCollision.OnFreeze.AddListener(ToggleFreeze);
        player = GameObject.FindWithTag("Player");
        timer = FindFirstObjectByType<Timer>();
        timer.enabled = true;
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
        Time.timeScale = 0f;
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
