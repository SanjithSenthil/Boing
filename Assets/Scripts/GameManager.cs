using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMPro.TMP_Text scoreText;
    public GameObject[] lifeHearts;

    public TMPro.TMP_Text livesText;
    public int score = 0;
    public int lives = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
        void Start()
    {
        UpdateHUD();
    }

   public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateHUD();
    }

    public void LoseLife()
    {
        if (lives <= 0) return;

        lives--;
        UpdateHearts();

        if (lives <= 0)
        {
            // Debug.Log("Game Over!");
            // if (gameOverUI != null)
            //     gameOverUI.SetActive(true);
        }
    }

    void UpdateHUD()
    {
        UpdateScoreUI();
        UpdateHearts();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < lifeHearts.Length; i++)
        {
            lifeHearts[i].SetActive(i < lives);
        }
    }

}
