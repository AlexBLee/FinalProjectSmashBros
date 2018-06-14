using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour 
{

	private Button button;
	public Cursor[] cursorList;
	

	// Use this for initialization
	void Start () 
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(GoToGame);
	}

	public void GoToGame()
	{
		if(GameManager.instance.ready == true)
		{
			SceneManager.LoadScene("Level1");
		}
	}


	

}
