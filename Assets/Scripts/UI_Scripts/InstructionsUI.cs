using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject dimOverlay; // Add this for the dim background
    [SerializeField] private PauseMenuUI pauseMenu;

    private void Awake()
    {
        HideInstructions(); // Ensures it's off initially
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
        dimOverlay.SetActive(true); // Enable dim overlay
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
        dimOverlay.SetActive(false); // Disable dim overlay
    }

    public void BackToPauseMenu()
    {
        HideInstructions();
        pauseMenu.ShowPausePanel();
    }
}
