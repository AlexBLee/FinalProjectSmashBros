using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public Button playButton;
    public Button trainingButton;
    public Button optionsButton;
    public Button quitButton;

    private const string TRAINING_MODE = "Training";

    void Start ()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(MainMenuUIManager.instance.ToPlayScreen);
        }

        if (trainingButton != null)
        {
            trainingButton.onClick.AddListener(ToTraining);
        }

        if (optionsButton != null) 
        {
            optionsButton.onClick.AddListener(MainMenuUIManager.instance.ToOptionsScreen);
        }

        if (quitButton != null)
        {
            optionsButton.onClick.AddListener(Quit);
        } 
    }

    private void ToTraining()
    {
        SceneManager.LoadScene(TRAINING_MODE);
    }

    private void Quit()
    {
        Application.Quit();
    }


    

}