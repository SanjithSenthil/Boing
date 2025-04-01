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
        //SceneManager.LoadScene("Level 1"); 
        if(SceneLoader.instance != null)
        {
            SceneLoader.instance.LoadSceneByName("Level 1");
            Debug.Log("Restarting game...");
        }
        else
        {
            Debug.LogWarning("SceneLoader instance is null! Cannot restart the game.");
        }       
    }

    public void GoToMainMenu()
    {
        Debug.Log("GoToMainMenu button clicked!");
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu"); 
        if(SceneLoader.instance != null)
        {
            SceneLoader.instance.LoadSceneByName("MainMenu");
            Debug.Log("Going to main menu...");
        }
        else
        {
            Debug.LogWarning("SceneLoader instance is null! Cannot go to main menu.");
        }
        GameData.Instance.levelScores = new int[2]; // Reset
    }
}
