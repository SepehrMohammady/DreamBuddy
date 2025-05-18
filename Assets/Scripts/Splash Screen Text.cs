using System.Collections; // Import the System.Collections namespace to enable usage of IEnumerable and IEnumerator
using System.IO; // Import the System.IO namespace to handle file operations
using TMPro; // Import the TMPro namespace to use TextMeshPro components
using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using UnityEngine.SceneManagement; // Import the UnityEngine.SceneManagement namespace to manage scenes in Unity

// Define a public class named SplashScreenText that inherits from MonoBehaviour
public class SplashScreenText : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public float typingDuration = 0.11f; // Time delay between each character being typed out
    public float startDelay = 3f; // Initial delay before typing starts
    public float endDelay = 3f; // Delay after typing finishes before loading the next scene
    public string lobbySceneName = "Lobby"; // Name of the Lobby scene to load if no key is found
    public string sesudapefoSceneName = "SESUDAPEFO"; // Name of the SESUDAPEFO scene
    public string sesudaalSceneName = "SESUDAAL"; // Name of the SESUDAAL scene
    public string sesunialSceneName = "SESUNIAL"; // Name of the SESUNIAL scene
    public string sesunipefoSceneName = "SESUNIPEFO"; // Name of the SESUNIPEFO scene
    public string underConstructionSceneName = "Under Construction"; // Name of the Under Construction scene
    public AudioClip textSound; // AudioClip to play while typing text
    private TMP_Text textMeshPro; // Reference to the TextMeshPro component
    private string message; // The message to be displayed
    private AudioSource audioSource; // Reference to the AudioSource component

    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Get the TextMeshPro component attached to this GameObject
        textMeshPro = GetComponent<TMP_Text>();

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // If there is no AudioSource component, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the textSound clip to the audio source
        audioSource.clip = textSound;

        // Get the message from the TextMeshPro component and clear the text
        message = textMeshPro.text;
        textMeshPro.text = "";

        // Start the coroutine to handle the typing effect
        StartCoroutine(StartTypingEffect());
    }

    // Coroutine to handle the initial delay and start typing effect
    IEnumerator StartTypingEffect()
    {
        // Wait for the specified initial delay
        yield return new WaitForSeconds(startDelay);

        // If there is a sound clip, play it once at the beginning
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Start the coroutine to type the text
        StartCoroutine(TypeText());
    }

    // Coroutine to handle the typing effect
    IEnumerator TypeText()
    {
        // Loop through each character in the message string
        foreach (char letter in message.ToCharArray())
        {
            // Add the character to the TextMeshPro text
            textMeshPro.text += letter;

            // Wait for the specified typing duration before adding the next character
            yield return new WaitForSeconds(typingDuration);
        }

        // Wait for the specified end delay after typing is complete
        yield return new WaitForSeconds(endDelay);

        // Load the next scene based on the generated key from Key.txt
        LoadNextScene();
    }

    // Method to load the next scene based on the generated key from Key.txt
    void LoadNextScene()
    {
        // Define the path to the Key.txt file
        string filePath = Path.Combine(Application.dataPath, "Saved", "Key.txt");

        // Check if the Key.txt file exists
        if (File.Exists(filePath))
        {
            // Read the contents of the Key.txt file
            string content = File.ReadAllText(filePath).Trim();

            // Extract the key by removing the "Key: " prefix
            string key = content.Replace("Key: ", "").Trim();

            // Use a switch statement to determine which scene to load based on the key
            switch (key)
            {
                case "SESUDAPEFO":
                    SceneManager.LoadScene(sesudapefoSceneName); // Load the SESUDAPEFO scene
                    break;
                case "SESUDAAL":
                    SceneManager.LoadScene(sesudaalSceneName); // Load the SESUDAAL scene
                    break;
                case "SESUNIAL":
                    SceneManager.LoadScene(sesunialSceneName); // Load the SESUNIAL scene
                    break;
                case "SESUNIPEFO":
                    SceneManager.LoadScene(sesunipefoSceneName); // Load the SESUNIPEFO scene
                    break;
                default:
                    SceneManager.LoadScene(underConstructionSceneName); // Load the Under Construction scene if the key is not recognized
                    break;
            }
        }
        else
        {
            // If the Key.txt file does not exist, load the Lobby scene
            SceneManager.LoadScene(lobbySceneName);
        }
    }
}
