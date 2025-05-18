using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using System.Collections; // Import the System.Collections namespace to enable usage of IEnumerable and IEnumerator

// Define a public class named Cinematic that inherits from MonoBehaviour
public class Cinematic : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public Animator cameraAnimator; // Reference to the Animator component to control camera animations
    public Camera playerCamera; // Reference to the player's camera
    public Camera cinematicCamera; // Reference to the cinematic camera
    public float cinematicDuration = 17f; // Duration of the cinematic sequence in seconds

    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Enable the player's camera and disable the cinematic camera at the start
        playerCamera.enabled = true;
        cinematicCamera.enabled = false;

        // Start the coroutine to initiate the cinematic sequence after a short delay
        StartCoroutine(StartCinematicAfterDelay(0.5f));
    }

    // Coroutine to start the cinematic sequence after a specified delay
    IEnumerator StartCinematicAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the player's camera and enable the cinematic camera
        playerCamera.enabled = false;
        cinematicCamera.enabled = true;

        // Trigger the animation for the cinematic sequence
        cameraAnimator.SetTrigger("StartCinematic");

        // Wait for the duration of the cinematic sequence
        yield return new WaitForSeconds(cinematicDuration);

        // Re-enable the player's camera and disable the cinematic camera after the cinematic sequence
        playerCamera.enabled = true;
        cinematicCamera.enabled = false;
    }
}
