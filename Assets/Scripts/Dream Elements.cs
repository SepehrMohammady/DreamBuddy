using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using TMPro; // Import the TMPro namespace to use TextMeshPro components
using UnityEngine.UI; // Import the UnityEngine.UI namespace to manage UI elements like buttons
using System.Collections; // Import the System.Collections namespace to enable usage of IEnumerable and IEnumerator
using System.IO; // Import the System.IO namespace to handle file operations

// Define a public class named DreamElements that inherits from MonoBehaviour
public class DreamElements : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component for displaying dialogue text
    public GameObject messageBox; // Reference to the message box GameObject
    public Button[] choiceButtons; // Array of choice buttons
    public TextMeshProUGUI[] choiceButtonTexts; // Array of TextMeshProUGUI components for the choice buttons
    public EntranceDoor entranceDoor; // Reference to the EntranceDoor script
    public BuddyInteraction buddyInteraction; // Reference to the BuddyInteraction script
    public MonoBehaviour playerCameraController; // Reference to the player camera controller script

    // Private variables to manage choices and coroutines
    private bool choiceMade = false; // Flag to check if a choice has been made
    private string selectedEnvironment = ""; // Store the selected environment choice
    private string selectedWeather = ""; // Store the selected weather choice
    private string selectedTimeOfDay = ""; // Store the selected time of day choice
    private string selectedCompany = ""; // Store the selected company choice
    private string selectedPet = ""; // Store the selected pet choice
    private string key = ""; // Store the generated key
    private Coroutine currentCoroutine = null; // Reference to the current running coroutine

    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Initially hide the dialogue text and message box
        dialogueText.gameObject.SetActive(false);
        messageBox.SetActive(false);

        // Initially hide all choice buttons
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // Method to start the questionnaire
    public void StartQuestionnaire()
    {
        // Return if a choice has already been made
        if (choiceMade)
            return;

        // Show the message box and display the initial dialogue
        messageBox.SetActive(true);
        dialogueText.text = "Buddy: I’ll ask you some simple questions to make your dream come true.";
        dialogueText.gameObject.SetActive(true);

        // Stop the BuddyInteraction conversation if it is active
        if (buddyInteraction != null)
        {
            buddyInteraction.StopConversation();
        }

        // Start the coroutine to show the next question after a delay
        currentCoroutine = StartCoroutine(ShowNextQuestionWithDelay(3f));
    }

    // Coroutine to show the next question after a specified delay
    IEnumerator ShowNextQuestionWithDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Show the next question
        ShowNextQuestion();
    }

    // Method to show the next question to the player
    void ShowNextQuestion()
    {
        // Return if a choice has already been made
        if (choiceMade)
            return;

        // Update the dialogue text and show the choices after a delay
        dialogueText.text = "Buddy: Choose the option that makes you feel free, relaxed, and without stress:";
        currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Sea", "Forest", "Space" }));
    }

    // Coroutine to show choices after a specified delay
    IEnumerator ShowChoicesAfterDelay(float delay, string[] choices)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Show the choices to the player
        ShowChoices(choices);

        // Freeze the camera when choices appear
        playerCameraController.enabled = false;
    }

    // Method to show choices to the player
    void ShowChoices(string[] choices)
    {
        // Update the dialogue text
        dialogueText.text = "Buddy: Choose the option that makes you feel free, relaxed, and without stress:";

        // Loop through each choice button and update its text and visibility
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtonTexts[i].text = choices[i];
                choiceButtons[i].gameObject.SetActive(true);
                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex, choices));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        // Show and unlock the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Method to handle the player's choice
    void OnChoiceSelected(int choiceIndex, string[] choices)
    {
        // Hide all choice buttons
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        // Get the selected choice and update the key
        string selectedChoice = choices[choiceIndex];
        key += selectedChoice.Substring(0, 2).ToUpper();

        // Update the dialogue text and show the next set of choices
        dialogueText.text = "Buddy: Choose the option that makes you feel free, relaxed, and without stress:";

        // Check and update the state based on the selected choice
        if (selectedEnvironment == "")
        {
            if (selectedChoice == "Sea" || selectedChoice == "Forest")
            {
                selectedEnvironment = selectedChoice;
                currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Sunny", "Rainy", "Snowy" }));
            }
            else if (selectedChoice == "Space")
            {
                selectedEnvironment = selectedChoice;
                currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Spaceship", "Moon", "Mars" }));
            }
        }
        else if (selectedWeather == "" && (selectedEnvironment == "Sea" || selectedEnvironment == "Forest"))
        {
            selectedWeather = selectedChoice;
            currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Day", "Night" }));
        }
        else if (selectedTimeOfDay == "")
        {
            selectedTimeOfDay = selectedChoice;
            currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Alone", "Pet" }));
        }
        else if (selectedCompany == "")
        {
            selectedCompany = selectedChoice;
            if (selectedCompany == "Pet")
            {
                currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Dog", "Cat", "Fox" }));
            }
            else
            {
                // Mark the choice as made and proceed to the next step
                choiceMade = true;
                messageBox.SetActive(false);
                dialogueText.gameObject.SetActive(false);
                Cursor.visible = false;
                playerCameraController.enabled = true;
                SaveKeyToFile(); // Save the generated key here
                StopCurrentCoroutine();
                if (entranceDoor != null)
                {
                    entranceDoor.ActivateDoor(key);
                }
                gameObject.SetActive(false);
            }
        }
        else
        {
            selectedPet = selectedChoice;
            choiceMade = true;
            messageBox.SetActive(false);
            dialogueText.gameObject.SetActive(false);
            Cursor.visible = false;
            playerCameraController.enabled = true;
            SaveKeyToFile(); // Save the generated key here
            StopCurrentCoroutine();
            if (entranceDoor != null)
            {
                entranceDoor.ActivateDoor(key);
            }
            gameObject.SetActive(false);
        }
    }

    // Method to stop the current running coroutine
    void StopCurrentCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

    // Method to save the generated key to a file and PlayerPrefs
    void SaveKeyToFile()
    {
        // Define the folder and file paths
        string folderPath = Path.Combine(Application.dataPath, "Saved");
        string filePath = Path.Combine(folderPath, "Key.txt");

        // Create the folder if it does not exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Write the key content to the file
        string content = $"Key: {key}";
        File.WriteAllText(filePath, content);

        // Save the key to PlayerPrefs
        PlayerPrefs.SetString("GeneratedKey", key);
        PlayerPrefs.Save();
    }
}
