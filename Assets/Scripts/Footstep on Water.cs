using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity
using StarterAssets; // Import the StarterAssets namespace for ThirdPersonController reference

// Define a public class named FootstepOnWater that inherits from MonoBehaviour
public class FootstepOnWater : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public AudioClip[] defaultFootstepSounds; // Array to store the default footstep sounds
    public AudioClip[] waterFootstepSounds; // Array to store the footstep sounds for walking on water
    public Collider lakeBoundary; // Reference to the collider that represents the lake boundary

    // Private variable to reference the ThirdPersonController component
    private ThirdPersonController thirdPersonController;

    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Get the ThirdPersonController component attached to the same GameObject
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // The OnTriggerEnter method is called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is the lake boundary
        if (other == lakeBoundary)
        {
            // Switch the footstep sounds to the water footstep sounds
            thirdPersonController.FootstepAudioClips = waterFootstepSounds;
        }
    }

    // The OnTriggerExit method is called when another collider exits the trigger collider attached to this GameObject
    void OnTriggerExit(Collider other)
    {
        // Check if the collider that exited the trigger is the lake boundary
        if (other == lakeBoundary)
        {
            // Switch the footstep sounds back to the default footstep sounds
            thirdPersonController.FootstepAudioClips = defaultFootstepSounds;
        }
    }
}
