using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    // Button
    public Button backButton;

    // Options
    public Slider audioSlider;
    public ToggleGroup difficultyToggleGroup;

    // --------------------------------------------------------------------------------------------------------- //    

	void Start ()
    {
        if (backButton != null) backButton.onClick.AddListener(BackButtonPressed);
        // if (audioSlider != null) audioSlider.onValueChanged.AddListener( (val) => { AudioSliderChanged(val); });
        if (audioSlider != null) audioSlider.onValueChanged.AddListener(AudioSliderChanged);
    }

    // Goes back to the default menu screen.
    private void BackButtonPressed()
    {
        MainMenuUIManager.instance.ToMainMenuScreen();
    }

    // Change Audio
    private void AudioSliderChanged(float val)
    {
        // Set a value in an options manager or audio manager class.
    }

    // Change difficulty?
    private void DifficultyToggled(bool easy)
    {
        // Set a value in an options manager or game manager class.
    }

}
