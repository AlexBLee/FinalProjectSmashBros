﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class LevelManager : NetworkBehaviour 
{
	// List that stays active across character select/game/end result screen

	// List for things in level.
	public List<GameObject> players;

	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;
	
	public Text text;
	

	void Awake () 
	{


		// If the players exist, instantiate them in their respective areas, add to the level list and set their spawn positions.

		// PLAYER 1
		if(GameManager.instance.players[0] != null)
		{
			GameObject player1 = Instantiate(GameManager.instance.players[0], spawns[0].position ,Quaternion.identity);
			player1.name = GameManager.instance.players[0].name;
			players.Add(player1);
			PlayerManager playerManager = player1.GetComponent<PlayerManager>();
			playerManager.spawnPosition = respawns[0];
			NetworkServer.Spawn(player1);
		}
		
		// PLAYER 2
		if(GameManager.instance.players[1] != null)
		{
			GameObject player2 = Instantiate(GameManager.instance.players[1], spawns[1].position ,Quaternion.identity);
			player2.name = GameManager.instance.players[1].name;
			players.Add(player2);
			PlayerManager playerManager = player2.GetComponent<PlayerManager>();
			playerManager.spawnPosition = respawns[1];
			NetworkServer.Spawn(player2);
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

	

	// When there is one more player remaining, end the game.
	private void Update() 
	{
		if(players.Count == 1)
		{
			SceneManager.LoadScene("EndResult");
		}
	}

	

}
