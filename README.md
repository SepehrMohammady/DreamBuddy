# DreamBuddy

![DreamBuddy Gameplay GIF](gameplay.gif)

The DreamBuddy project is a simulator designed to help players relax and de-stress by creating virtual environments tailored to their preferences. The core idea is to provide an escape for individuals experiencing mental fatigue or stress, allowing them to rejuvenate in a scenario resembling their dream place of relaxation.

This project was developed by Sepehr Mohammady as part of the "Computer Games and Simulation" course at the University of Genoa.

**Play the Prototype:** [Download the latest build from GitHub Releases](https://github.com/SepehrMohammady/DreamBuddy/releases/tag/v0.1.0)

## Project Concept

The player begins their journey by interacting with an NPC named Buddy. Buddy asks the player a series of questions to understand their preferences and desired environment. Based on the player's responses, Buddy guides them to a specific scenario that best matches their dream place of relaxation. This process involves selecting options from predefined choices provided by Buddy.

## Key Components (Scripts)

The core logic of DreamBuddy is managed by the following C# scripts:

*   **`BuddyInteraction.cs`**: Manages the dialogue between Buddy and the player, presenting choices and processing responses.
*   **`DreamElements.cs`**: Handles the questionnaire process, determining a "key" based on player choices, and activating the corresponding scenario.
*   **`EntranceDoor.cs`**: Manages the transition between scenes based on the generated key, directing the player to the appropriate scenario.

## Technologies Used

*   Unity Game Engine (6000.X.Xf1)
*   C# Programming Language

## Setup and Running the Project from Source

This repository contains the essential scripts and project files to open and explore the DreamBuddy project in the Unity Editor.

**Prerequisites:**
*   Unity Hub
*   Unity Editor (Version 6000.X.Xf1 or compatible)

**Steps:**
1.  Clone the repository:
    ```bash
    git clone https://github.com/SepehrMohammady/DreamBuddy.git
    cd DreamBuddy
    ```
2.  Open the project in Unity Hub by clicking "Open" and navigating to the cloned `DreamBuddy` folder.
3.  **Important: Required Asset Store Packages:** To fully experience the scenes as intended, you will need to import the following packages from the Unity Asset Store into the project via the Package Manager or by direct import:
    *   [Native Cursor] - [Link to Asset Store page](https://assetstore.unity.com/packages/tools/utilities/native-cursor-220347)
    *   [URP Tree Models] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/vegetation/trees/urp-tree-models-253340)
    *   [Conifers [BOTD]] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/vegetation/trees/conifers-botd-142076)
    *   [Grass And Flowers Pack 1] - [Link to Asset Store page](https://assetstore.unity.com/packages/2d/textures-materials/nature/grass-and-flowers-pack-1-17100)
    *   [Grass Flowers Pack Free] - [Link to Asset Store page](https://assetstore.unity.com/packages/2d/textures-materials/nature/grass-flowers-pack-free-138810)
    *   [Simple Water Shader URP] - [Link to Asset Store page](https://assetstore.unity.com/packages/2d/textures-materials/water/simple-water-shader-urp-191449)
    *   [Automatic Revolving Door] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/props/furniture/automatic-revolving-door-153549)
    *   [Industrial Props Kit] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/props/industrial/industrial-props-kit-84745)
    *   [POLYGRUNT - Construction Vehicles] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/vehicles/land/polygrunt-construction-vehicles-168884)
    *   [Wooden House - Free - Low Poly] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/environments/wooden-house-free-low-poly-270889)
    *   [Quirky Series - FREE Animals Pack] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/characters/animals/quirky-series-free-animals-pack-178235)
    *   [Butterfly (Animated)] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/characters/animals/insects/butterfly-animated-58355)
    *   [Toon Fox] - [Link to Asset Store page](https://assetstore.unity.com/packages/3d/characters/animals/toon-fox-183005)
    *   [Fantasy Skybox FREE] - [Link to Asset Store page](https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353)
    *   [Textures] - [Link to Asset Store page](https://polyhaven.com/textures)

    
    *   If you do not import these, scenes may appear with missing textures (pink) or missing objects. The core scripts will still be present.
4.  Once the project is open and required assets are imported, you can explore the scenes located in the `Assets/Scenes/` folder and run the game in the editor.

## Playable Build

A playable prototype build is available:
*   **[Download from GitHub Releases](URL_TO_YOUR_GITHUB_RELEASE_PAGE_FOR_DREAMBUDDY)**

## Current Limitations

*   **Manual Scenario Creation:** Each relaxation scenario currently requires manual creation within the Unity Editor, limiting the scalability and variety of instantly available environments.
*   **Predefined Choices:** Player interaction for personalization relies on selecting from predefined options, which restricts the depth of personalization compared to free-form input.

## Future Development Ideas

To enhance the project and make it more scalable and personalized, the following improvements are proposed:

1.  **AI-Powered NPC:** Replace the current NPC with an AI-powered one capable of understanding natural language input, allowing players to describe their dream locations more freely.
2.  **Dynamic Scenario Generation:** Define game elements (environments, objects, ambiances) separately and use AI to dynamically assemble scenarios based on player input, enabling a vast number of unique experiences.
3.  **Psychology-Driven Suggestions:** Train the AI with psychological principles to offer suggestions that could enhance the therapeutic aspect of the simulator based on player's stated needs.
4.  **Real-Time Monitoring and Feedback:** Incorporate (optional) biometric feedback (e.g., heart rate through wearables if feasible) to adapt the experience in real-time.
5.  **Enhanced Interaction:** Implement voice-based interactions for a more immersive experience.
6.  **Virtual Reality (VR) Integration:** Adapt the project for VR to provide a more profound sense of presence and immersion.

## Project Report

For a detailed overview of the project's conception, implementation, and discussion, please see the [DreamBuddy Project Report](Documents/Report.pdf).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
