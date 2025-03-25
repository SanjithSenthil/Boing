using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;
    public int score = 0;
    public int lives = 3;
    private GameObject player;

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
        player = GameObject.FindWithTag("Player");
    }

   public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateHUD();
    }

    public void LoseLife()
    {
        lives--;
        UpdateHUD();
        if (lives <= 0)
        {
            // basic Game over without hud
            Debug.Log("Game Over!");
            Destroy(player);
        }
    }

    void UpdateHUD()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        if (livesText != null)
            livesText.text = "LIVES: " + lives;
    }
}
