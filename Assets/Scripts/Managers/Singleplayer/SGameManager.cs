using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameManager : MonoBehaviour 
{
	// Singleton to bring things between scenes.
	public static SGameManager instance;

	// -----------------|
	// Character Select |
	// -----------------|

	// -- Character Select options
	public enum gamemode { KO = 0, Timer = 1 };
	public int gameModeNumber = 0;
	public List<GameObject> players;
	public bool ready;

	//------------------|
	// In-Game			|
	//------------------|

	// --- For loading the players into the right positions.
	public int p1Number;
	public int p2Number;
	
	// --- Lives and Timer
	public int lives = 5;
	public int timeInSeconds = 5;

	// ------------------|
	// Results Screen	 |
	// ------------------|

	// From going from In-Game to results screen
	public List<Stats> placeList;
	

	// -----------------------------------------------------------------------------------------------------------------------//

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

	
	// QUIT the game.
	private void Update() {
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void ResetValues()
	{
		gameModeNumber = 0;

		players.Clear();
		
		for(int i = 0; i < 2; i++)
		{
			players.Add(null);
		}

		ready = false;

		p1Number = 0;
		p2Number = 0;

		lives = 5;
		timeInSeconds = 5;

		placeList.Clear();

	}

	// -----------------------------------------------------------------------------------------------------------------------//



}
