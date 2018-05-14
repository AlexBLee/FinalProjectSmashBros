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

	void GoToGame()
	{
		SceneManager.LoadScene("TestMap");
	}


	

}
