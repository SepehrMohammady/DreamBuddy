using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using TMPro; // Import the TMPro namespace to use TextMeshPro components
using System.Collections; // Import the System.Collections namespace to enable usage of IEnumerable and IEnumerator

// Define a public class named EntranceDoor that inherits from MonoBehaviour
public class EntranceDoor : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public GameObject entranceDoor; // Reference to the entrance door GameObject
    public GameObject doorBoundary; // Reference to the door boundary GameObject
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component for displaying dialogue text
    public GameObject messageBox; // Reference to the message box GameObject
    public BuddyInteraction buddyInteraction; // Reference to the BuddyInteraction script
    public string destinationSceneSESUDAPEFO; // Name of the SESUDAPEFO destination scene
    public string destinationSceneSESUDAAL; // Name of the SESUDAAL destination scene
    public string destinationSceneSESUNIAL; // Name of the SESUNIAL destination scene
    public string destinationSceneSESUNIPEFO; // Name of the SESUNIPEFO destination scene
    public string destinationSceneUnderConstruction; // Name of the Under Construction destination scene
    public GameObject player; // Reference to the player GameObject

    // Private variables to manage state
    private string key = ""; // Variable to store the received key
    private bool isPlayerWithinBoundary = false; // Flag to check if the player is within the boundary

    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Initially hide the entrance door, door boundary, and message box
        entranceDoor.SetActive(false);
        doorBoundary.SetActive(false);
        messageBox.SetActive(false);
    }

    // The Update method is called once per frame
    void Update()
    {
        // Check if the player is within the door boundary
        if (IsPlayerWithinBoundary(doorBoundary))
        {
            // If the player has just entered the boundary, log the event and start the delayed load
            if (!isPlayerWithinBoundary)
            {
                Debug.Log("Player has entered the door boundary.");
                StartCoroutine(DelayedLoadDestination(1.5f)); // Start coroutine with a 1.5 seconds delay
                isPlayerWithinBoundary = true; // Set the flag to true
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

    // Method to activate the door with the received key
    public void ActivateDoor(string receivedKey)
    {
        // Store the received key
        key = receivedKey;

        // Show the entrance door and door boundary
        entranceDoor.SetActive(true);
        doorBoundary.SetActive(true);

        // Start the coroutine to show the door dialogue
        StartCoroutine(ShowDoorDialogue());
    }

    // Coroutine to show the door dialogue
    IEnumerator ShowDoorDialogue()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Show the message box and display the dialogue
        messageBox.SetActive(true);
        dialogueText.text = "Buddy: Open the door behind you. I’ll be there for you…";
        dialogueText.gameObject.SetActive(true);

        // Wait for 1 second before hiding the dialogue and message box
        yield return new WaitForSeconds(1f);
        messageBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        // Stop the BuddyInteraction script to avoid conflicts
        if (buddyInteraction != null)
        {
            buddyInteraction.StopConversation();
            buddyInteraction.gameObject.SetActive(false);
        }
    }

    // Coroutine to delay the loading of the destination scene
    IEnumerator DelayedLoadDestination(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the appropriate destination scene based on the key
        LoadDestination();
    }

    // Method to load the destination scene based on the key
    void LoadDestination()
    {
        // Use a switch statement to determine which scene to load based on the key
        switch (key)
        {
            case "SESUDAPEFO":
                UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneSESUDAPEFO); // Load the SESUDAPEFO scene
                break;
            case "SESUDAAL":
                UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneSESUDAAL); // Load the SESUDAAL scene
                break;
            case "SESUNIAL":
                UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneSESUNIAL); // Load the SESUNIAL scene
                break;
            case "SESUNIPEFO":
                UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneSESUNIPEFO); // Load the SESUNIPEFO scene
                break;
            default:
                UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneUnderConstruction); // Load the Under Construction scene if the key is not recognized
                break;
        }
    }
}
