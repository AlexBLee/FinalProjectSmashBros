using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Timer : MonoBehaviour 
{
	// Text
	private Text text;

    // --------------------------------------------------------------------------------------------------------- //    

	void Awake()
	{
		text = GetComponent<Text>();

		// If the mode isn't the timed mode, then don't use the timer.
		if(GameManager.instance.gameModeNumber == 0)
		{
			gameObject.SetActive(false);
		}
		
	}
	
	
	// Display time elapsed.
	void Update () 
	{
		float guiTime = Time.timeSinceLevelLoad - (GameManager.instance.timeInSeconds * 60); 

		int minutes = Mathf.Abs((int)guiTime / 60);
		int seconds = Mathf.Abs((int)guiTime % 60);
		int fraction = Mathf.Abs((int)(guiTime*10) % 10);

		text.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);


	}
}
