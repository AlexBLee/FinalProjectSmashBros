using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSwitcher : MonoBehaviour 
{
	private GameManager instance;
	public Text mode;
	public Text minLives;

	private void Start() {
		instance = GameManager.instance;
	}


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

	public void CheckMode()
	{
		if(instance.gameModeNumber == (int)GameManager.gamemode.KO)
		{
			mode.text = "Survival Fest!";
			minLives.text = GameManager.instance.lives + " lives";
		}

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
