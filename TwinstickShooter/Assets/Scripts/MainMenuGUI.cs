using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour
{
    public int buttonWidth = 100;
    public int buttonHeight = 30;

    public GUIStyle titleStyle;
    public GUIStyle buttonStyle;

    void OnGUI()
    {
        // Get the center of our screen.
        var buttonX = (Screen.width - buttonWidth)/2.0f;
        var buttonY = (Screen.height - buttonHeight)/2.0f;

        // Show butotn on the screen and check if clicked.
        if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Start Game", buttonStyle))
        {
            // If button clicked, load the game.
            Application.LoadLevel("Chapter_01");
        }

        // Add a game's title to the game, above our button.
        GUI.Label(new Rect(buttonX + 2.5f, buttonY - 50,
            110.0f, 20.0f), "Twinstick Shooter", titleStyle);
    }
}
