using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SPause : MonoBehaviour 
{
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
			Time.timeScale = 0.0f;
			text.SetActive(true);

			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				SceneManager.LoadScene("SCharacterSelect");
			}
		}

		if(!paused)
		{
			text.SetActive(false);
			Time.timeScale = 1.0f;
		}



	}
}
