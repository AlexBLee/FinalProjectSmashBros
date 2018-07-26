using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

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
		if(GameManager.instance.ready == true)
		{
			//SceneManager.LoadScene(gameObject.name);
			NetworkManager.singleton.ServerChangeScene(gameObject.name);

		}
	}
}
