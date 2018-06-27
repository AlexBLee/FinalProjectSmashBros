using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    // Menu Manager
    public static MainMenuUIManager instance;

    // Menu screens.
    public PlayScreen playScreen;
    public MainMenuScreen mainMenuScreen;
    public OptionsScreen optionsScreen;

    // Title screen.
    public RectTransform titleRT;

    // The menu screen that you're currently on.
    private RectTransform currentScreen;

    // --------------------------------------------------------------------------------------------------------- //    
    
    private void Awake()
    {
        instance = this;
    }

    // Set the current screen to the title screen to begin.
    private void Start() 
    {
        currentScreen = titleRT;
    }

    // Go to menu screens when called.
    public void ToMainMenuScreen()
    {
        ChangeScreen(mainMenuScreen.GetComponent<RectTransform>());
    }

    public void ToPlayScreen()
    {
        ChangeScreen(playScreen.GetComponent<RectTransform>());
    }

    public void ToOptionsScreen()
    {
        ChangeScreen(optionsScreen.GetComponent<RectTransform>());
    }

    // Using LeanTween - to animate screens to go to the side.
    private void ChangeScreen(RectTransform nextScreen)
    {
        if(nextScreen != currentScreen)
        {
            float xPos = nextScreen.rect.width;
            nextScreen.position = new Vector3(xPos*1.5f, nextScreen.position.y, nextScreen.position.z);
            // tween current screen out and deactivate
            LeanTween.moveX(currentScreen, xPos, 1.0f).setOnComplete(() => 
            {
                currentScreen.gameObject.SetActive(false);
                // activate and tween next screen in
                nextScreen.gameObject.SetActive(true);
                LeanTween.moveX(nextScreen, 0.0f, 1.0f).setOnComplete(() => { currentScreen = nextScreen; });
            });

            // Set the current screen to the screen that you're going to be on.
            currentScreen = nextScreen;
        }
    }
}
