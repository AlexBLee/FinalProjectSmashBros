using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    // Easiest, laziest, and worst way to make a Singleton.
    public static MainMenuUIManager instance;

    public PlayScreen playScreen;
    public MainMenuScreen mainMenuScreen;
    public OptionsScreen optionsScreen;

    public RectTransform titleRT;

    private RectTransform currentScreen;

    private void Awake()
    {
        instance = this;
    }

    private void Start() 
    {
        currentScreen = titleRT;
    }

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

            currentScreen = nextScreen;
        }
    }
}
