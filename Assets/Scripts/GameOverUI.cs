using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject gameOverPanel;
    
    public void ShowGameOver(int score)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"FINAL SCORE: {score}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // replace with your actual main menu scene name
    }
}
