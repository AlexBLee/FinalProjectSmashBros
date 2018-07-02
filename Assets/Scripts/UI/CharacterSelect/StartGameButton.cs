using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class StartGameButton : NetworkBehaviour 
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
		if(GameManager.instance.ready == true)
		{
			//SceneManager.LoadScene("LevelSelect");
			NetworkManager.singleton.ServerChangeScene("LevelSelect");
		}
	}
	



	

}
