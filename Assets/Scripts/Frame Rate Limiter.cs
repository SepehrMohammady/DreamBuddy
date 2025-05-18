using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity

// Define a public class named FrameRateLimiter that inherits from MonoBehaviour
public class FrameRateLimiter : MonoBehaviour
{
    // The Start method is called once when the script instance is being loaded
    void Start()
    {
        // Set the target frame rate for the application to 90 frames per second
        Application.targetFrameRate = 90;
    }
}
