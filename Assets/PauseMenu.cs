using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Reference to the Pause Menu UI Canvas
    private bool isPaused = false; // Variable to track the pause state

    void Start()
    {
        // Make sure the pause menu is inactive at the start
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1; // Ensure the game is unpaused
    }

    void Update()
    {
        // Check for escape key press to toggle pause state
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true); // Activate the pause menu
        Time.timeScale = 0; // Pause the game
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false); // Deactivate the pause menu
        Time.timeScale = 1; // Resume the game
    }
}
