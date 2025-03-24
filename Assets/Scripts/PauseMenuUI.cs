using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private InstructionsUI instructionsUI;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = FindFirstObjectByType<InputManager>();

        if (inputManager != null)
            inputManager.OnPauseToggle.AddListener(TogglePause);

        pausePanel.SetActive(false);
        instructionsUI.HideInstructions(); // Hide on start
    }

    private void TogglePause()
    {
        if (GameManager.instance.lives <= 0) return;

        if (isPaused) Resume();
        else Pause();
    }

    public void Resume()
    {
        instructionsUI.HideInstructions();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        instructionsUI.HideInstructions();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowInstructions()
    {
        pausePanel.SetActive(false);
        instructionsUI.ShowInstructions();
    }

    public void ShowPausePanel()
{
    pausePanel.SetActive(true);
}


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
