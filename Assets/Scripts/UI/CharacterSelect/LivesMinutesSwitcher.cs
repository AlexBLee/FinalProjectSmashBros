using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesMinutesSwitcher : MonoBehaviour 
{
	private GameManager instance;
	private int gameModeNumber;

	private void Start() 
	{
		instance = GameManager.instance;
		instance.lives = 5;
		instance.timeInSeconds = 5;
		
	}

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
