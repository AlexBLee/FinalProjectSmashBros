using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// Singleton to bring things between scenes.

	// Character Select/In-Game
	public static GameManager instance;
	public List<GameObject> players;
	public bool ready;
	
	public int p1Number;
	public int p2Number;

	// From going from In-Game to results screen
	public int p1Kills;
	public int p2Kills;
	
	public int p1Deaths;
	public int p2Deaths;


	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

	}
	
	private void Update() {
		if(Input.GetKey(KeyCode.Escape))
		{
			QuitGame();
		}
	}

	void QuitGame()
	{
		Application.Quit();
	}

}
