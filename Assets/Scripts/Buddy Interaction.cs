using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using TMPro; // Import the TMPro namespace to use TextMeshPro components
using UnityEngine.UI; // Import the UnityEngine.UI namespace to manage UI elements like buttons
using System.Collections; // Import the System.Collections namespace to enable usage of IEnumerable and IEnumerator
using System.IO; // Import the System.IO namespace to handle file operations

// Define a public class named BuddyInteraction that inherits from MonoBehaviour
public class BuddyInteraction : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component for displaying dialogue text
    public GameObject messageBox; // Reference to the message box GameObject
    public Button[] choiceButtons; // Array of choice buttons
    public TextMeshProUGUI[] choiceButtonTexts; // Array of TextMeshProUGUI components for the choice buttons
    public MonoBehaviour playerCameraController; // Reference to the player camera controller script
    public DreamElements dreamElements; // Reference to the DreamElements script

    // Private variables to manage conversation state and coroutines
    private bool isWaitingForInput = false; // Flag to check if the script is waiting for user input
    private bool conversationCompleted = false; // Flag to check if the conversation is completed
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

        // Initially hide the cursor
        Cursor.visible = false;
    }

    // The Update method is called once per frame
    void Update()
    {
        // Check if the script is waiting for user input
        if (isWaitingForInput && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            // Show the next dialogue when the user presses Return, Space, or Left Mouse Button
            ShowNextDialogue();
        }
    }

    // Method to start the conversation
    public void StartConversation()
    {
        // Return if the conversation is already completed
        if (conversationCompleted)
            return;

        // Start the coroutine to handle the conversation sequence
        currentCoroutine = StartCoroutine(ConversationSequence());
    }

    // Coroutine to handle the conversation sequence
    IEnumerator ConversationSequence()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Show the message box and display the first dialogue
        messageBox.SetActive(true);
        dialogueText.text = "Buddy: Hello dreamer! Welcome to your dream!";
        dialogueText.gameObject.SetActive(true);
        isWaitingForInput = true;
        Cursor.visible = true;

        // Wait for 3 seconds before checking for input
        yield return new WaitForSeconds(3f);
        if (isWaitingForInput)
        {
            ShowNextDialogue();
        }
    }

    // Method to show the next dialogue based on the current dialogue text
    void ShowNextDialogue()
    {
        // Return if the conversation is already completed
        if (conversationCompleted)
            return;

        // Check the current dialogue text and update it accordingly
        if (dialogueText.text == "Buddy: Hello dreamer! Welcome to your dream!")
        {
            dialogueText.text = "Buddy: I know, I know, this place is not your dream but the place you start to create your dream.";
            isWaitingForInput = true;
            currentCoroutine = StartCoroutine(WaitForInputOrTimeout(6f));
        }
        else if (dialogueText.text == "Buddy: I know, I know, this place is not your dream but the place you start to create your dream.")
        {
            dialogueText.text = "Buddy: By the way, how do you feel right now?";
            isWaitingForInput = false;
            currentCoroutine = StartCoroutine(ShowChoicesAfterDelay(0.5f, new string[] { "Awful", "Sad", "Normal", "Happy", "Awesome" }, true));
        }
    }

    // Coroutine to wait for user input or timeout
    IEnumerator WaitForInputOrTimeout(float timeout)
    {
        float timer = 0f;
        while (timer < timeout && isWaitingForInput)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if (isWaitingForInput)
        {
            ShowNextDialogue();
        }
    }

    // Coroutine to show choices after a delay
    IEnumerator ShowChoicesAfterDelay(float delay, string[] choices, bool isFeelingsChoice)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Show the choices to the player
        ShowChoices(choices, isFeelingsChoice);

        // Freeze the camera when choices appear
        playerCameraController.enabled = false;
    }

    // Method to show choices to the player
    void ShowChoices(string[] choices, bool isFeelingsChoice)
    {
        // Update the dialogue text if the choices are related to feelings
        if (isFeelingsChoice)
        {
            dialogueText.text = "Buddy: By the way, how do you feel right now?";
        }

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
    public void OnChoiceSelected(int choiceIndex, string[] choices)
    {
        // Hide all choice buttons and the message box
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
        messageBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        // Save the player's choice to a file
        SaveChoiceToFile(choices[choiceIndex]);

        // Hide and lock the cursor
        Cursor.visible = false;

        // Enable the player camera controller
        playerCameraController.enabled = true;

        // Continue the dialogue after the choice is made
        currentCoroutine = StartCoroutine(ContinueDialogueAfterChoice(choices[choiceIndex]));
    }

    // Coroutine to continue the dialogue after the player makes a choice
    IEnumerator ContinueDialogueAfterChoice(string choice)
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Show the message box and display the next dialogue
        messageBox.SetActive(true);
        dialogueText.text = "Buddy: I hope after this experience you feel better than now!";
        dialogueText.gameObject.SetActive(true);

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);
        dialogueText.text = "Buddy: For the start, let me introduce myself.";

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        dialogueText.text = "Buddy: I’m Buddy, as your buddy!";

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        dialogueText.text = "Buddy: Yes, I am Buddy, and of course your dream buddy! Let's get to the point.";

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Hide the message box and dialogue text
        messageBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        // Stop the conversation and start the questionnaire
        StopConversation();
        dreamElements.StartQuestionnaire();
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

    // Method to save the player's choice to a file
    void SaveChoiceToFile(string choice)
    {
        string folderPath = Path.Combine(Application.dataPath, "Saved");
        string filePath = Path.Combine(folderPath, "Feelings.txt");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string content = $"Before: {choice}\nAfter: ";
        File.WriteAllText(filePath, content);
    }

    // Method to stop the conversation
    public void StopConversation()
    {
        // Stop the current running coroutine
        StopCurrentCoroutine();

        // Mark the conversation as completed and reset input flag
        conversationCompleted = true;
        isWaitingForInput = false;
    }
}
