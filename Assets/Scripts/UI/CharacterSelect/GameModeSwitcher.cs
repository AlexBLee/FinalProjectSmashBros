using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSwitcher : MonoBehaviour 
{
	public Text mode;
	public Text minLives;
	public int modeNum;

	private void Start() {
		modeNum = (int)GameManager.gamemode.KO;
	}


	public void SwitchMode()
	{
		modeNum++;
		CheckMode();

		if(modeNum > 1)
		{
			modeNum = 0;
			CheckMode();
		}
	}

	public void CheckMode()
	{
		if(modeNum == (int)GameManager.gamemode.KO)
		{
			mode.text = "Survival Fest!";
			minLives.text = GameManager.instance.lives + " lives";
		}

		if(modeNum == (int)GameManager.gamemode.Timer)
		{
			mode.text = "KO Fest!";
			minLives.text = GameManager.instance.timeInSeconds + "-mins";
		}
	}

	private void Update() {
		CheckMode();
	}
}
