using System;
using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    enum Menu
    {
        None,
        Pause,
        Options,
        Credits
    };

    static public bool isPaused;

    public float windowWidth = 256;
    public float windowHeight = 100;

    public GUISkin newSkin;

    Menu currentMenu;

    void Start()
    {
        // We don't want the game paused when it starts and/ or resets.
        isPaused = false;
        currentMenu = Menu.None;
    }

    void OnGUI()
    {
        // Set the GUI's default skin to the one we set here
        GUI.skin = newSkin;

        if (isPaused && currentMenu == Menu.None)
            currentMenu = Menu.Pause;

        if (currentMenu == Menu.None)
        {
            Time.timeScale = 1.0f;
            return;
        }

        // We're at a menu, so let's pause the game.
        Time.timeScale = 0.0f;

        switch (currentMenu)
        {
            case Menu.Options:
                ShowOptionsMenu();
                break;
            case Menu.Credits:
                ShowCreditsScreen();
                break;
            case Menu.Pause:
                ShowPauseMenu();
                break;
        }
    }

    void BuildWindow()
    {
        var windowX = (Screen.width - windowWidth)/2;
        var windowY = (Screen.height - windowHeight)/2;

        /* 
            What's insane about this is the `windowHeight * 2` portion.
        
            So, the pause menu 'Back' button, for some reason get's cut off, so I figure I'll increase the height.

            Baring in mind that windowHeight = 100...

            If I _set_ windowHeight to 200, it does nothing.
            If I multiply windowHeight by 2, so it equals TWO-HUNDRED then it works.

            Big old #wtf going on there.

            (Unity 5.0.1f1 Personal).
        */
        
        GUILayout.BeginArea(new Rect(windowX, windowY, windowWidth, windowHeight * 3));
    }

    void ShowCreditsScreen()
    {
        BuildWindow();

        // Instead of the default blank background,
        // we will use what the GUISkin uses for the box properties.
        GUILayout.BeginVertical("box");

        GUILayout.Label("By Kieron Lanning");
        GUILayout.Label("From the Unity Developer Blueprints book");
        GUILayout.Label("by Packt Publishing.");

        if (GUILayout.Button("Back"))
            currentMenu = Menu.Pause;

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void ShowOptionsMenu()
    {
        BuildWindow();

        // Instead of the default blank background,
        // we will use what the GUISkin uses for the box properties.
        GUILayout.BeginVertical("box");

        // Set our volume
        GUILayout.Label("Master Volume - (" + AudioListener.volume.ToString("f2") + ")");

        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0.0f, 1.0f);

        var backgroundVolume = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        GUILayout.Label("Music Volume - (" + backgroundVolume.volume.ToString("f2") + ")");

        backgroundVolume.volume = GUILayout.HorizontalSlider(backgroundVolume.volume, 0.0f, 1.0f);

        // Display and add the ability to change graphics quality.
        var currentQualitySetting = QualitySettings.GetQualityLevel();
        var qualityName = QualitySettings.names[currentQualitySetting];

        GUILayout.Label("Quality - " + qualityName);

        GUILayout.Label("Spaceship Colour:");

        var playerRenderer = GameObject.Find("playerShip").GetComponent<Renderer>();

        GUILayout.BeginHorizontal();

        GUILayout.Label("r:", new GUIStyle { fixedWidth = 10, normal = new GUIStyleState { textColor = Color.white } });
        var r = GUILayout.HorizontalSlider(playerRenderer.material.color.r, 0.0f, 1.0f);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("b:", new GUIStyle { fixedWidth = 10, normal = new GUIStyleState { textColor = Color.white} });

        var b = GUILayout.HorizontalSlider(playerRenderer.material.color.g, 0.0f, 1.0f);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        GUILayout.Label("g:", new GUIStyle { fixedWidth = 10, normal = new GUIStyleState { textColor = Color.white } });

        var g = GUILayout.HorizontalSlider(playerRenderer.material.color.b, 0.0f, 1.0f);

        GUILayout.EndHorizontal();

        playerRenderer.material.color = new Color(r, g, b);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Decrease"))
        {
            QualitySettings.DecreaseLevel();
        }

        if (GUILayout.Button("Increase"))
        {
            QualitySettings.IncreaseLevel();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Back"))
        {
            currentMenu = Menu.Pause;
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void ShowPauseMenu()
    {
        BuildWindow();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Resume"))
        {
            // Resume the game.
            isPaused = false;
            currentMenu = Menu.None;
        }

        if (GUILayout.Button("Restart"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Options"))
        {
            currentMenu = Menu.Options;
        }

        if (GUILayout.Button("Credits"))
        {
            currentMenu = Menu.Credits;
        }

        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Main Menu"))
        {
            Application.LoadLevel("Main_Menu");
        }

        if (GUILayout.Button("Exit Game"))
        {
            // Only works when published.
            Application.Quit();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}