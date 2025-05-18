using StarterAssets; // Import the StarterAssets namespace for ThirdPersonController reference
using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using UnityEngine.SceneManagement; // Import the UnityEngine.SceneManagement namespace to manage scenes in Unity
using UnityEngine.UI; // Import the UnityEngine.UI namespace to manage UI elements like buttons

// Define a public class named PauseMenu that inherits from MonoBehaviour
public class PauseMenu : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public GameObject menuPanel; // Reference to the menu panel GameObject
    public Button resumeButton; // Reference to the Resume button
    public Button restartButton; // Reference to the Restart button
    public Button exitButton; // Reference to the Exit button
    public GameObject player; // Reference to the player GameObject

    // Private variable to track whether the game is paused
    private bool isPaused = false;

    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Hide the menu panel at the start
        menuPanel.SetActive(false);

        // Add listeners to the buttons to handle click events
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);

        // Lock and hide the cursor at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // The Update method is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle between pausing and resuming the game
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

    // Method to resume the game
    void ResumeGame()
    {
        // Hide the menu panel
        menuPanel.SetActive(false);

        // Resume the game by setting the time scale to 1
        Time.timeScale = 1f;

        // Update the isPaused flag
        isPaused = false;

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable player controls
        player.GetComponent<ThirdPersonController>().enabled = true;
    }

    // Method to pause the game
    void PauseGame()
    {
        // Show the menu panel
        menuPanel.SetActive(true);

        // Pause the game by setting the time scale to 0
        Time.timeScale = 0f;

        // Update the isPaused flag
        isPaused = true;

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable player controls
        player.GetComponent<ThirdPersonController>().enabled = false;
    }

    // Method to restart the game
    void RestartGame()
    {
        // Ensure the game is unpaused before restarting
        Time.timeScale = 1f;

        // Load the Lobby scene
        SceneManager.LoadScene("Lobby");
    }

    // Method to exit the game
    void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
