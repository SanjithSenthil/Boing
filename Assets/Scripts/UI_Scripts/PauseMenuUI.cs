using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject dimOverlay;
    [SerializeField] private InstructionsUI instructionsUI;
    private InputManager inputManager;
    [Header("Sound Effects")]
    [SerializeField] private AudioClip uiSound; // Single sound for both ESC and button clicks

    private void Start()
    {
        inputManager = FindFirstObjectByType<InputManager>();

        if (inputManager != null)
            inputManager.OnPauseToggle.AddListener(TogglePause);

        pausePanel.SetActive(false);
        instructionsUI.HideInstructions();
        dimOverlay.SetActive(false);
    }

    private void TogglePause()
    {
        PlayUISound();
        if (GameManager.instance.lives <= 0) return;

        if (isPaused) Resume();
        else Pause();
    }

    public void Resume()
    {
        if (AudioManager.instance != null)
    {
        AudioManager.instance.RestoreMusicVolume(); 
    }
        instructionsUI.HideInstructions();
        pausePanel.SetActive(false);
        dimOverlay.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        if (AudioManager.instance != null)
    {
        AudioManager.instance.LowerMusicVolume(); 
    }
        instructionsUI.HideInstructions();
        pausePanel.SetActive(true);
        dimOverlay.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowInstructions()
    {
        PlayUISound();
        pausePanel.SetActive(false);
        instructionsUI.ShowInstructions();
        dimOverlay.SetActive(true);
    }

    public void ShowPausePanel()
    {
        PlayUISound();
        pausePanel.SetActive(true);
        dimOverlay.SetActive(true);
    }

    public void GoToMainMenu()
    {
        PlayUISound();
        // Reset the game data and scores
        GameData.Instance.ResetScore();
    
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");

        // Check if SceneLoader instance exists and call LoadNextScene
        if (SceneLoader.instance != null)
        {
            SceneLoader.instance.LoadSceneByName("MainMenu"); // This will load the next scene (e.g., MainMenu)
        }
        else
        {
            Debug.LogError("SceneLoader instance is missing!");
        }
        if (AudioManager.instance != null)
        {
            AudioManager.instance.RestoreMusicVolume(); 
        }
    }
    public void PlayUISound()
    {
        if (uiSound != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(uiSound);
        }
    }

}
