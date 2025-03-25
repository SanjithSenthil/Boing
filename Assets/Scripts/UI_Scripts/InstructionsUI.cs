using UnityEngine;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private PauseMenuUI pauseMenu;

    private void Awake()
    {
        HideInstructions(); // Ensures it's off initially
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        HideInstructions();
        pauseMenu.ShowPausePanel(); // 👈 calls a method in PauseMenuUI
    }
}
