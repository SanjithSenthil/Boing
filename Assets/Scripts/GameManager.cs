using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private CoinCounterUI coinCounter;
    [SerializeField] private GameObject[] lifeHearts;

    [Header("Player Stats")]
    public int score = 0;
    public int lives = 3;
    private GameObject player;
    private bool isCooldown = false;

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
        if (isCooldown || lives <= 0) return;

        lives--;
        UpdateHearts();
        StartCoroutine(LoseLifeCooldown());

        if (lives <= 0)
        {
            Time.timeScale = 1f; 
            GameData.finalScore = score;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            Destroy(player);
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
}
