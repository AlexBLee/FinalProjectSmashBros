using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// Singleton to bring things between scenes.
	public static GameManager instance;

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
	public int p1Kills;
	public int p2Kills;
	
	public int p1Deaths;
	public int p2Deaths;

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

	// -----------------------------------------------------------------------------------------------------------------------//



}
