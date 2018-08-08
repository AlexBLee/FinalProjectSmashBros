using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SPause : MonoBehaviour 
{
	public bool paused = false;

	void Update () 
	{
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


		if(paused)
		{
			Time.timeScale = 0.0f;

			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				SceneManager.LoadScene("SCharacterSelect");
			}
		}

		if(!paused)
		{
			Time.timeScale = 1.0f;
		}

		Debug.Log(paused);


	}
}
