﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SLevelSelect : MonoBehaviour {

	// Button
	private Button button;
	
    // --------------------------------------------------------------------------------------------------------- //    

	// Use this for initialization
	void Start () 
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(GoToLevel);
	}

	// Go to the level.
	public void GoToLevel()
	{

		SceneManager.LoadScene(gameObject.name);

	}
}
