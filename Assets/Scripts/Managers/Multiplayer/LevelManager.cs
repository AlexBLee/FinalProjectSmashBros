using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class LevelManager : NetworkBehaviour 
{
	// Struct for finding network ID's
	public struct ID
	{
		public NetworkInstanceId netID;

		public ID(NetworkInstanceId id)
		{
			netID = id;
		}
	}

	// Sync Structs
	public class SyncPlayers : SyncListStruct<ID>
	{
		
	}

	// Game over check
	public GameObject gameOverText;

	// Countdown timer before game start
	public TextMeshProUGUI countdownText;

	// List for things in level.
	public List<GameObject> players;
	public SyncPlayers syncPlayers = new SyncPlayers();

	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;

	// For timer condition
	public Timer timer;
	[SyncVar]
	public bool countdown;

	
	

	// -----------------------------------------------------------------------------------------------------------------------//

	private void Start() 
	{
		// To stop the main menu music.
		Destroy(GameObject.Find("Music"));
		timer = FindObjectOfType<Timer>();

		players = FindObjectOfType<CameraScript>().players;
		countdown = true;
		StartCoroutine(StartLevel());
	}
	
	// When there is one more player remaining, end the game.
	private void Update() 
	{
		if(countdown)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;

		// Looking for two, as there is an extra object that is neccesary but isn't a player.
		if(GameManager.instance.gameModeNumber == 0)
		{
			if(players.Count == 2)
			{
				StartCoroutine(SwitchScene());
			}
		}
		else if(GameManager.instance.gameModeNumber == 1)
		{
			if(timer != null && timer.done)
			{
				StartCoroutine(SwitchScene());
			}
		}
	}



	
	[Server]
	public void CmdSpawnUnits()
	{
		// If the players exist, instantiate them in their respective areas, add to the level list and set their spawn positions.

		// PLAYER 1
		if(GameManager.instance.players[0] != null)
		{
			GameObject player1 = Instantiate(GameManager.instance.players[0], spawns[0].position ,Quaternion.identity);
			player1.name = GameManager.instance.players[0].name;
			NetworkServer.Spawn(player1);
			NetworkServer.ReplacePlayerForConnection(NetworkServer.connections[0], player1, 0);
            CmdAddToList(player1.GetComponent<NetworkIdentity>().netId);

		}
		
		// PLAYER 2
		if(GameManager.instance.players[1] != null)
		{
			GameObject player2 = Instantiate(GameManager.instance.players[1], spawns[1].position ,Quaternion.identity);
			player2.name = GameManager.instance.players[1].name;
			NetworkServer.Spawn(player2);
			NetworkServer.ReplacePlayerForConnection(NetworkServer.connections[1], player2, 1);
            CmdAddToList(player2.GetComponent<NetworkIdentity>().netId);

		}

	}

	// Add players to list.
	[ClientRpc]
	void RpcAddToList(NetworkInstanceId id)
	{
		syncPlayers.Add(new ID(id));
	}

	[Command]
	void CmdAddToList(NetworkInstanceId id)
	{
		syncPlayers.Add(new ID(id));
	}

	IEnumerator SwitchScene()
	{
		gameOverText.SetActive(true);
		yield return new WaitForSecondsRealtime(4);
		NetworkManager.singleton.ServerChangeScene("EndResult");

	}

	// Level start countdown
	IEnumerator StartLevel()
	{

		countdownText.text = "3";
		yield return new WaitForSecondsRealtime(1);
		countdownText.text = "2";
		yield return new WaitForSecondsRealtime(1);
		countdownText.text = "1";
		yield return new WaitForSecondsRealtime(1);
		countdownText.text = "START!";
		yield return new WaitForSecondsRealtime(1);
		countdownText.gameObject.SetActive(false);

		countdown = false;


	}






}


 

 

