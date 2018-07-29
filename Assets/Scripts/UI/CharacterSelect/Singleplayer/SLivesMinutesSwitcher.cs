using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLivesMinutesSwitcher : MonoBehaviour 
{
	// Game Manager
	private SGameManager instance;

    // --------------------------------------------------------------------------------------------------------- //    

	private void Start() 
	{
		instance = SGameManager.instance;

		// Defaults.
		instance.lives = 5;
		instance.timeInSeconds = 5;
		
	}


	// Add number accordingly to mode.
	// Maximum lives is 10, maximum time is 15 minutes.
	public void AddNumber()
	{
		if(SGameManager.instance.gameModeNumber == (int)SGameManager.gamemode.KO)
		{
			if(instance.lives < 10)
			{
				instance.lives++;
			}
		}

		if(SGameManager.instance.gameModeNumber == (int)SGameManager.gamemode.Timer)
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
		if(SGameManager.instance.gameModeNumber == (int)SGameManager.gamemode.KO)
		{
			if(instance.lives > 1)
			{
				instance.lives--;
			}
		}

		if(SGameManager.instance.gameModeNumber == (int)SGameManager.gamemode.Timer)
		{
			if(instance.timeInSeconds > 1)
			{
				instance.timeInSeconds--;
			}
		}
	}
	
}
