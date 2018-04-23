using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Timer : MonoBehaviour 
{
	private float startTime;
	private Text text;

	void Awake()
	{
		startTime = 420;
		text = GetComponent<Text>();
		
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		float guiTime = Time.time - startTime; 

		int minutes = Mathf.Abs((int)guiTime / 60);
		int seconds = Mathf.Abs((int)guiTime % 60);
		int fraction = Mathf.Abs((int)(guiTime*10) % 10);

		text.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);


	}
}
