using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesMinutesSwitcher : MonoBehaviour 
{
	GameModeSwitcher gameModeSwitcher;
	GameManager instance;

	private void Start() 
	{
		gameModeSwitcher = FindObjectOfType<GameModeSwitcher>();
		instance = GameManager.instance;
		instance.lives = 5;
		instance.timeInSeconds = 5;
	}

	public void AddNumber()
	{
		if(gameModeSwitcher.modeNum == (int)GameManager.gamemode.KO)
		{
			if(instance.lives < 10)
			{
				instance.lives++;
			}
		}

		if(gameModeSwitcher.modeNum == (int)GameManager.gamemode.Timer)
		{
			if(instance.timeInSeconds < 15)
			{
				instance.timeInSeconds++;
			}
		}
	}

	public void MinusNumber()
	{
		if(gameModeSwitcher.modeNum == (int)GameManager.gamemode.KO)
		{
			if(instance.lives > 1)
			{
				instance.lives--;
			}
		}

		if(gameModeSwitcher.modeNum == (int)GameManager.gamemode.Timer)
		{
			if(instance.timeInSeconds > 1)
			{
				instance.timeInSeconds--;
			}
		}
	}
	
}
