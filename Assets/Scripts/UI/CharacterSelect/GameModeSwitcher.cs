using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSwitcher : MonoBehaviour 
{
	// Game Manager
	private GameManager instance;

	// Text for the mode chosen.
	public Text mode;

	// To display the lives or time.
	public Text minLives;

    // --------------------------------------------------------------------------------------------------------- //    

	private void Start() 
	{
		instance = GameManager.instance;
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
		if(instance.gameModeNumber == (int)GameManager.gamemode.KO)
		{
			mode.text = "Survival Fest!";
			minLives.text = GameManager.instance.lives + " lives";
		}

		// Timed battle with time.
		if(instance.gameModeNumber == (int)GameManager.gamemode.Timer)
		{
			mode.text = "KO Fest!";
			minLives.text = GameManager.instance.timeInSeconds + "-mins";
		}
	}

	private void Update() {
		CheckMode();
	}
}
