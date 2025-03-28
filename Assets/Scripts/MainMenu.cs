using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject rulesPanel;
    public GameObject mainMenuPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); // Ensure the name matches your Level 1 scene
    }

    public void ShowRules()
    {
        Debug.Log("ShowRules function called");
        rulesPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideRules()
    {
        Debug.Log("HideRules function called");
        mainMenuPanel.SetActive(true);
        rulesPanel.SetActive(false);
    }
}
