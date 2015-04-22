using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    static public bool isPaused;

    public float windowWidth = 256;
    public float windowHeight = 100;

    public GUISkin newSkin;

    void Start()
    {
        // We don't want the game paused when it starts and/ or resets.
        isPaused = false;
    }

    void OnGUI()
    {
        // Set the GUI's default skin to the one we set here
        GUI.skin = newSkin;

        if (isPaused)
        {
            // First, we pause the game.
            Time.timeScale = 0.0f;

            // Then we need to display the pause menu.
            ShowPauseMenu();
        }
        else
        {
            // Make the game run like normal.
            Time.timeScale = 1.0f;
        }
    }

    void ShowPauseMenu()
    {
        // Then we need to display the pause menu.
        var windowX = (Screen.width - windowWidth)/2.0f;
        var windowY = (Screen.height - windowHeight)/2.0f;

        GUILayout.BeginArea(new Rect(windowX, windowY, windowWidth, windowHeight));

        if (GUILayout.Button("Resume"))
        {
            // Resume the game.
            isPaused = false;
        }

        if (GUILayout.Button("Main Menu"))
        {
            Application.LoadLevel("Main_Menu");
        }

        if (GUILayout.Button("Exit Game"))
        {
            // Only works when published.
            Application.Quit();
        }

        GUILayout.EndArea();
    }
}