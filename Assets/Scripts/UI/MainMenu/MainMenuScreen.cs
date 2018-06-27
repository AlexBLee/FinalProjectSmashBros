using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    // Buttons
    public Button playButton;
    public Button trainingButton;
    public Button optionsButton;
    public Button quitButton;

    // Tag
    private const string TRAINING_MODE = "Training";
    
    // --------------------------------------------------------------------------------------------------------- //    


    void Start ()
    {
        // Add button listeners.
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

    // If the player clicks training, go to training.
    private void ToTraining()
    {
        SceneManager.LoadScene(TRAINING_MODE);
    }

    // Exit the game.
    private void Quit()
    {
        Application.Quit();
    }


    

}