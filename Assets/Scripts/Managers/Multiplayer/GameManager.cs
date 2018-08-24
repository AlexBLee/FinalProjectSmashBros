using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class GameManager : NetworkBehaviour 
{


	public class SyncListStats : SyncListStruct<Stats>
	{

	}

	
	// Singleton to bring things between scenes.
	public static GameManager instance;

	// -----------------|
	// Character Select |
	// -----------------|

	// -- Character Select options
	public enum gamemode { KO = 0, Timer = 1 };

	[SyncVar]
	public int gameModeNumber = 0;
	
	public List<GameObject> players;

	[SyncVar]
	public bool ready;

	[SyncVar]
	public int playerNumber = 2;

	[SyncVar]
	public Vector2 spawn = new Vector2(-0.7f,0.55f);

	//------------------|
	// In-Game			|
	//------------------|

	// --- For loading the players into the right positions.
	[SyncVar]
	public int p1Number;
	[SyncVar]
	public int p2Number;
	
	// --- Lives and Timer
	[SyncVar]
	public int lives = 5;
	[SyncVar]
	public int timeInSeconds = 5;

	// ------------------|
	// Results Screen	 |
	// ------------------|

	// From going from In-Game to results screen
	public SyncListStats placeList = new SyncListStats();


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

	public void ResetValues()
	{
		gameModeNumber = 0;
		players.Clear();
		for(int i = 0; i < 2; i++)
		{
			players.Add(null);
		}
		
		ready = false;

		playerNumber = 2;

		lives = 5;
		timeInSeconds = 5;

		placeList.Clear();

	}
	

	// -----------------------------------------------------------------------------------------------------------------------//



}
