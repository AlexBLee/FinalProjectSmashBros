using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class LobbyCustom : NetworkLobbyManager
{
	// Keep track of which player connected.
	int playerNumber = 0;
	GameObject chosenCharacter; // character1, character2, etc.

	public override void OnLobbyClientConnect(NetworkConnection conn)
	{
		Debug.Log("Player Connected!");

		IntegerMessage msg = new IntegerMessage(playerNumber);
        //ClientScene.AddPlayer(conn, 0, msg);
	}

	//
	public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
	{
		Vector2 spawn = GameManager.instance.spawn;
        int n = GameManager.instance.playerNumber;

         //Select the prefab from the spawnable objects list
         var playerPrefab = spawnPrefabs[n];
  
         // Create player object with prefab
         var player = Instantiate(playerPrefab, spawn, Quaternion.identity) as GameObject;        
         
		GameManager.instance.spawn.x += 1.2f;
        GameManager.instance.playerNumber++;

		return player;

         // Add player object for connection

	}

	public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	{
		// PLAYER 1
		if(GameManager.instance.players[0] != null)
		{
			GameObject player1 = Instantiate(GameManager.instance.players[0], Vector2.zero ,Quaternion.identity);
			Debug.Log("!");
			NetworkServer.SpawnWithClientAuthority(player1, conn);
			Debug.Log("!!");

			chosenCharacter = player1;
		}
		
		return chosenCharacter;
	}
	
	
}
