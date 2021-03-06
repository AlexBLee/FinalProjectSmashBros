﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : NetworkBehaviour 
{
	[SyncVar]
	public bool paused = false;
	public GameObject text;

	void Update () 
	{
		// Pause the game
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!paused)
			{
				paused = true;
			}
			else
			{
				paused = false;
			}
		}

		// If backspace is clicked when paused, go back to character select.
		if(paused)
		{
			text.SetActive(true);
			Time.timeScale = 0.0f;

			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				NetworkManager.singleton.ServerChangeScene("EndResult");
			}
		}

		if(!paused)
		{
			text.SetActive(false);
			Time.timeScale = 1.0f;
		}



	}
}
