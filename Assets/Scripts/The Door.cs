using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using System.Collections; // Import the System.Collections namespace to enable usage of IEnumerable and IEnumerator

// Define a public class named TheDoor that inherits from MonoBehaviour
public class TheDoor : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public GameObject doorBoundary; // Reference to the door boundary GameObject
    public GameObject player; // Reference to the player GameObject
    public string lobbySceneName; // Name of the Lobby scene to load after the player passes through the door twice

    // Private variables to manage state
    private int passCount = 0; // Counter to keep track of how many times the player has passed through the door boundary
    private bool isPlayerWithinBoundary = false; // Flag to check if the player is currently within the boundary

    // The Update method is called once per frame
    void Update()
    {
        // Check if the player is within the door boundary
        if (IsPlayerWithinBoundary(doorBoundary))
        {
            // If the player has just entered the boundary
            if (!isPlayerWithinBoundary)
            {
                // Increment the pass count
                passCount++;
                Debug.Log($"Player has passed the door boundary {passCount} times."); // Log the number of times the player has passed the door boundary

                // If the player has passed the door boundary twice
                if (passCount == 2)
                {
                    Debug.Log("Player is being transported back to the Lobby scene."); // Log the event of transporting the player back to the Lobby scene
                    StartCoroutine(TransportToLobby()); // Start the coroutine to transport the player to the Lobby scene
                }

                // Set the flag to true to indicate that the player is within the boundary
                isPlayerWithinBoundary = true;
            }
        }
        else
        {
            // Reset the flag if the player leaves the boundary
            isPlayerWithinBoundary = false;
        }
    }

    // Method to check if the player is within a specified boundary
    bool IsPlayerWithinBoundary(GameObject boundary)
    {
        // Check if the player's position is within the bounds of the boundary's collider
        return boundary.GetComponent<Collider>().bounds.Contains(player.transform.position);
    }

    // Coroutine to transport the player to the Lobby scene after a delay
    IEnumerator TransportToLobby()
    {
        // Wait for 1 second before loading the Lobby scene
        yield return new WaitForSeconds(1f);

        // Load the Lobby scene using the SceneManager
        UnityEngine.SceneManagement.SceneManager.LoadScene(lobbySceneName);
    }
}
