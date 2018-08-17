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
				Time.timeScale = 0.0f;

			}
			else
			{
				paused = false;
				Time.timeScale = 1.0f;

			}
		}

		// If backspace is clicked when paused, go back to character select.
		if(paused)
		{
			text.SetActive(true);

			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				SceneManager.LoadScene("SCharacterSelect");
			}
		}

		if(!paused)
		{
			text.SetActive(false);
		}



	}
}
