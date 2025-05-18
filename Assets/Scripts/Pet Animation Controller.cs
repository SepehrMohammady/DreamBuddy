using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity

// Define a public class named PetAnimationController that inherits from MonoBehaviour
public class PetAnimationController : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public GameObject player; // Reference to the player GameObject
    public GameObject pet; // Reference to the pet GameObject
    public GameObject[] stepBoxes; // Array of step box GameObjects that define boundaries for triggering animations

    // Define a nested class to store animation steps
    [System.Serializable]
    public class AnimationStep
    {
        public AnimationClip[] animationClips; // Array to store animation clips for each step
    }

    // Public array of AnimationStep to define multiple animation steps for the pet
    public AnimationStep[] petAnimations;

    // Private variable to track the current step
    private int currentStep = 0;

    // The Update method is called once per frame
    void Update()
    {
        // Check if there are more steps to process and if the player is within the current step boundary
        if (currentStep < stepBoxes.Length && IsPlayerWithinBoundary(stepBoxes[currentStep]))
        {
            // Start animations for the current step
            StartAnimations(currentStep);

            // Move to the next step
            currentStep++;
        }
    }

    // Method to check if the player is within a specified boundary
    bool IsPlayerWithinBoundary(GameObject boundary)
    {
        // Check if the player's position is within the bounds of the boundary's collider
        return boundary.GetComponent<Collider>().bounds.Contains(player.transform.position);
    }

    // Method to start animations for the specified step index
    void StartAnimations(int stepIndex)
    {
        // Get the Animation component attached to the pet GameObject
        Animation petAnimation = pet.GetComponent<Animation>();

        // Check if the Animation component is found and the step index is valid
        if (petAnimation != null && stepIndex < petAnimations.Length)
        {
            // Loop through each animation clip in the specified step
            foreach (AnimationClip clip in petAnimations[stepIndex].animationClips)
            {
                // Check if the animation clip is not already added to the Animation component
                if (!petAnimation.GetClip(clip.name))
                {
                    // Add the animation clip to the Animation component
                    petAnimation.AddClip(clip, clip.name);
                }

                // Play the animation clip
                petAnimation.Play(clip.name);
            }
        }
        else
        {
            // Log a warning if the Animation component or animation clips are not found
            Debug.LogWarning("Pet animation component or animation clips not found.");
        }
    }
}
