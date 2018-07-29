using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SStartGameButton : MonoBehaviour 
{
	// Button
	private Button button;
	
    // --------------------------------------------------------------------------------------------------------- //    

	// Use this for initialization
	void Start () 
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(GoToLevelSelect);
	}

	// Go to level select.
	public void GoToLevelSelect()
	{
		if(SGameManager.instance.ready == true)
		{
			SceneManager.LoadScene("SLevelSelect");
		}
	}


	

}
