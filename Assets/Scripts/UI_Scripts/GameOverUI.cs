using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;

    void Start()
    {
        int totalScore = GameData.Instance.TotalScore();
        finalScoreText.text = $"FINAL SCORE: {totalScore}";
    }


    public void RestartGame()
    {
        Debug.Log("Restart button clicked!");
        Time.timeScale = 1f;
        GameData.Instance.levelScores = new int[2]; // Reset 
        SceneManager.LoadScene("Level 1"); 
    }

    public void GoToMainMenu()
    {
        Debug.Log("GoToMainMenu button clicked!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
        GameData.Instance.levelScores = new int[2]; // Reset
    }
}
