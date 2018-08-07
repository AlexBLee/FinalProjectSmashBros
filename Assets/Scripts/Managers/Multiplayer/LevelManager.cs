using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

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

	// List for things in level.
	public List<GameObject> players;
	public SyncPlayers syncPlayers = new SyncPlayers();

	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;
	
	// Text
	public Text text;

	private void Start() {
		// To stop the main menu music.
		Destroy(GameObject.Find("Music"));

		players = FindObjectOfType<CameraScript>().players;
	}
	
	// When there is one more player remaining, end the game.
	private void Update() 
	{
		// Looking for two, as there is an extra object that is neccesary but isn't a player.
		if(players.Count == 2)
		{
			NetworkManager.singleton.ServerChangeScene("EndResult");
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
			NetworkServer.Spawn(player1);
			NetworkServer.ReplacePlayerForConnection(NetworkServer.connections[0], player1, 0);
            CmdAddToList(player1.GetComponent<NetworkIdentity>().netId);

		}
		
		// PLAYER 2
		if(GameManager.instance.players[1] != null)
		{
			GameObject player2 = Instantiate(GameManager.instance.players[1], spawns[1].position ,Quaternion.identity);
			NetworkServer.Spawn(player2);
			NetworkServer.ReplacePlayerForConnection(NetworkServer.connections[1], player2, 1);
            CmdAddToList(player2.GetComponent<NetworkIdentity>().netId);

		}

	}

	[ClientRpc]
	void RpcAddToList(NetworkInstanceId id)
	{
		syncPlayers.Add(new ID(id));
	}

	[Command]
	void CmdAddToList(NetworkInstanceId id)
	{
		syncPlayers.Add(new ID(id));
		//RpcAddToList(id);
	}




}


 

 

