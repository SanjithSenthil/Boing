using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private CoinCounterUI coinCounter;
    [SerializeField] private GameObject[] lifeHearts;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private PauseMenuUI pauseMenuUI;


    [Header("Player Stats")]
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        gameOverUI?.gameObject.SetActive(false); // hides at start
        pauseMenuUI?.gameObject.SetActive(false); // hides at start
        UpdateHUD();
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

        if (lives <= 0 && gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true); // ✅ 
            Time.timeScale = 0f; 
            gameOverUI.ShowGameOver(score);
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
            coinCounter.UpdateScore(score); // Make sure you're calling UpdateScore() not AnimateScore()
    }

    private void UpdateHearts()
    {
        if (lifeHearts == null || lifeHearts.Length == 0) return;

        for (int i = 0; i < lifeHearts.Length; i++)
        {
            lifeHearts[i].SetActive(i < lives);
        }
    }
}
