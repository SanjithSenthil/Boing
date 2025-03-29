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
        GameData.Instance.levelScores[GameData.Instance.currentLevelIndex] = score;
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
            GameData.Instance.levelScores[GameData.Instance.currentLevelIndex] = score;
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
}
