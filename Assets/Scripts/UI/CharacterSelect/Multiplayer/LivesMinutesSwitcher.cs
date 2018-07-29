using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesMinutesSwitcher : MonoBehaviour 
{
	// Game Manager
	private GameManager instance;

    // --------------------------------------------------------------------------------------------------------- //    

	private void Start() 
	{
		instance = GameManager.instance;

		// Defaults.
		instance.lives = 5;
		instance.timeInSeconds = 5;
		
	}


	// Add number accordingly to mode.
	// Maximum lives is 10, maximum time is 15 minutes.
	public void AddNumber()
	{
		if(GameManager.instance.gameModeNumber == (int)GameManager.gamemode.KO)
		{
			if(instance.lives < 10)
			{
				instance.lives++;
			}
		}

		if(GameManager.instance.gameModeNumber == (int)GameManager.gamemode.Timer)
		{
			if(instance.timeInSeconds < 15)
			{
				instance.timeInSeconds++;
			}
		}
	}

	// Subtract number according to mode.
	public void MinusNumber()
	{
		if(GameManager.instance.gameModeNumber == (int)GameManager.gamemode.KO)
		{
			if(instance.lives > 1)
			{
				instance.lives--;
			}
		}

		if(GameManager.instance.gameModeNumber == (int)GameManager.gamemode.Timer)
		{
			if(instance.timeInSeconds > 1)
			{
				instance.timeInSeconds--;
			}
		}
	}
	
}
