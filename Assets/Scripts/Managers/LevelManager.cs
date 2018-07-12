using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class LevelManager : NetworkBehaviour 
{

	// List for things in level.
	public List<GameObject> players;
	public List<GameObject> syncPlayers;


	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;
	
	// Text
	public Text text;
	
	
	void Start () 
	{

	}
	

	// When there is one more player remaining, end the game.
	private void Update() 
	{
		if(players.Count == 99)
		{
			SceneManager.LoadScene("EndResult");
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
			NetworkServer.ReplacePlayerForConnection(NetworkServer.connections[0], player1, 0);
			player1.name = GameManager.instance.players[0].name;
			syncPlayers.Add(player1);
			PlayerManager playerManager = player1.GetComponent<PlayerManager>();
			playerManager.spawnPosition = respawns[0];

		}
		
		// PLAYER 2
		if(GameManager.instance.players[1] != null)
		{
			GameObject player2 = Instantiate(GameManager.instance.players[1], spawns[1].position ,Quaternion.identity);
			NetworkServer.Spawn(player2);
			NetworkServer.ReplacePlayerForConnection(NetworkServer.connections[1], player2, 1);
			player2.name = GameManager.instance.players[1].name;
			syncPlayers.Add(player2);
			PlayerManager playerManager = player2.GetComponent<PlayerManager>();
			playerManager.spawnPosition = respawns[1];

		}


		
		// PLAYER 3
		// if(GameManager.instance.players[2] != null)
		// {
		// 	GameObject player3 = Instantiate(GameManager.instance.players[2], spawns[2].position ,Quaternion.identity);
		// 	player3.name = GameManager.instance.players[2].name;
		// 	PlayerManager playerManager = player3.GetComponent<PlayerManager>();
		// 	playerManager.spawnPosition = respawns[2];
		
		// }
		
		// PLAYER 4
		// if(GameManager.instance.players[3] != null)
		// {
		// 	GameObject player4 = Instantiate(GameManager.instance.players[3], spawns[3].position ,Quaternion.identity);
		// 	player4.name = GameManager.instance.players[3].name;
	 	// 	PlayerManager playerManager = player4.GetComponent<PlayerManager>();
		// 	playerManager.spawnPosition = respawns[3];
		
		// }

	}

	[ClientRpc]
	public void RpcAddToList()
	{
		if(!isServer)
		{
			Debug.Log("not server!");
		}
		foreach(GameObject t in syncPlayers)
		{
			players.Add(t);
		}
	}

	

}
