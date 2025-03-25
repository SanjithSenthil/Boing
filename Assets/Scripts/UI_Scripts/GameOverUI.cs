using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;

    void Start()
    {
        finalScoreText.text = $"FINAL SCORE: {GameData.finalScore}";
    }


    public void RestartGame()
    {
        Debug.Log("Restart button clicked!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1"); 
    }

    public void GoToMainMenu()
    {
        Debug.Log("GoToMainMenu button clicked!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}
