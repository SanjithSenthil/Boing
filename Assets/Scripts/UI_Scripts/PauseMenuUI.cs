using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject dimOverlay; // ✅ Add this to manage the dim overlay
    [SerializeField] private InstructionsUI instructionsUI;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = FindFirstObjectByType<InputManager>();

        if (inputManager != null)
            inputManager.OnPauseToggle.AddListener(TogglePause);

        pausePanel.SetActive(false);
        instructionsUI.HideInstructions();
        dimOverlay.SetActive(false); // Ensure the overlay is hidden at start
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
        dimOverlay.SetActive(false); // ✅ Hide dim overlay
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        instructionsUI.HideInstructions();
        pausePanel.SetActive(true);
        dimOverlay.SetActive(true); // ✅ Show dim overlay
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowInstructions()
    {
        pausePanel.SetActive(false);
        instructionsUI.ShowInstructions();
        dimOverlay.SetActive(true); // ✅ Ensure the dim overlay is active
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        dimOverlay.SetActive(true); // ✅ Ensure the dim overlay is active
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
