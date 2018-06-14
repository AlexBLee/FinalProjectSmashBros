using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	private Button button;
	

	// Use this for initialization
	void Start () 
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(GoToLevel);
	}

	public void GoToLevel()
	{
		if(GameManager.instance.ready == true)
		{
			SceneManager.LoadScene(gameObject.name);
		}
	}
}
