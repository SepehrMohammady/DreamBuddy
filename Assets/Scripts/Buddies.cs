using UnityEngine; // Import the UnityEngine namespace, which contains essential classes and functions for Unity

// Define a public class named Buddies that inherits from MonoBehaviour
public class Buddies : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public GameObject sphere; // Reference to the sphere GameObject
    public GameObject sphereBoundary; // Reference to the sphere boundary GameObject
    public GameObject cube; // Reference to the cube GameObject
    public GameObject cubeBoundary; // Reference to the cube boundary GameObject
    public GameObject tetrahedron; // Reference to the tetrahedron GameObject
    public GameObject tetrahedronBoundary; // Reference to the tetrahedron boundary GameObject
    public GameObject player; // Reference to the player GameObject
    public BuddyInteraction buddyInteraction; // Reference to the BuddyInteraction script

    // Private variable to track whether dialogue is active
    private bool isDialogueActive = false;

    // The Update method is called once per frame
    void Update()
    {
        // Check if dialogue is not currently active
        if (!isDialogueActive)
        {
            // Check if the player is within the cube boundary
            if (IsPlayerWithinBoundary(cubeBoundary))
            {
                // Deactivate other shapes and activate the cube
                DeactivateShapes(cube, cubeBoundary);
            }
            // Check if the player is within the sphere boundary
            else if (IsPlayerWithinBoundary(sphereBoundary))
            {
                // Deactivate other shapes and activate the sphere
                DeactivateShapes(sphere, sphereBoundary);
            }
            // Check if the player is within the tetrahedron boundary
            else if (IsPlayerWithinBoundary(tetrahedronBoundary))
            {
                // Deactivate other shapes and activate the tetrahedron
                DeactivateShapes(tetrahedron, tetrahedronBoundary);
            }
        }
    }

    // Method to check if the player is within a specified boundary
    bool IsPlayerWithinBoundary(GameObject boundary)
    {
        // Check if the player's position is within the bounds of the boundary's collider
        return boundary.GetComponent<Collider>().bounds.Contains(player.transform.position);
    }

    // Method to deactivate other shapes and start dialogue for the active shape
    void DeactivateShapes(GameObject activeShape, GameObject activeBoundary)
    {
        // Activate the specified shape and its boundary
        activeShape.SetActive(true);
        activeBoundary.SetActive(true);

        // Deactivate the sphere and its boundary if the active shape is not the sphere
        if (activeShape != sphere)
        {
            sphere.SetActive(false);
            sphereBoundary.SetActive(false);
        }

        // Deactivate the cube and its boundary if the active shape is not the cube
        if (activeShape != cube)
        {
            cube.SetActive(false);
            cubeBoundary.SetActive(false);
        }

        // Deactivate the tetrahedron and its boundary if the active shape is not the tetrahedron
        if (activeShape != tetrahedron)
        {
            tetrahedron.SetActive(false);
            tetrahedronBoundary.SetActive(false);
        }

        // Set the isDialogueActive flag to true to prevent further shape activation
        isDialogueActive = true;

        // Start the conversation using the BuddyInteraction script
        buddyInteraction.StartConversation();
    }
}
