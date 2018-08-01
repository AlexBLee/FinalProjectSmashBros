using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SGameModeSwitcher : MonoBehaviour 
{
	// Game Manager
	private SGameManager instance;

	// Text for the mode chosen.
	public TextMeshProUGUI mode;

	// To display the lives or time.
	public TextMeshProUGUI minLives;

    // --------------------------------------------------------------------------------------------------------- //    

	private void Start() 
	{
		instance = SGameManager.instance;
	}

	// When the UI button is clicked, increment by 1 to display the right mode.
	public void SwitchMode()
	{
		instance.gameModeNumber++;
		CheckMode();

		if(instance.gameModeNumber > 1)
		{
			instance.gameModeNumber = 0;
			CheckMode();
		}
	}

	// Check which mode to play and display their options.
	public void CheckMode()
	{
		// Surivival mode with lives.
		if(instance.gameModeNumber == (int)SGameManager.gamemode.KO)
		{
			mode.text = "Survival Fest!";
			minLives.text = SGameManager.instance.lives + " lives";
		}

		// Timed battle with time.
		if(instance.gameModeNumber == (int)SGameManager.gamemode.Timer)
		{
			mode.text = "KO Fest!";
			minLives.text = SGameManager.instance.timeInSeconds + "-mins";
		}
	}

	private void Update() {
		CheckMode();
	}
}
