using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SLevelManager : MonoBehaviour 
{
	// List that stays active across character select/game/end result screen
	private List<GameObject> managerObjects;

	// List for things in level.
	public List<GameObject> players;

	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;
	
	// -----------------------------------------------------------------------------------------------------------------------//	

	void Awake () 
	{
		// To stop the main menu music.
		Destroy(GameObject.Find("Music"));

		managerObjects = SGameManager.instance.players;

		// If the players exist, instantiate them in their respective areas, add to the level list and set their spawn positions.

		// PLAYER 1
		if(managerObjects[0] != null)
		{
			GameObject player1 = Instantiate(managerObjects[0], spawns[0].position ,Quaternion.identity);
			player1.name = managerObjects[0].name;
			players.Add(player1);
			SPlayerManager playerManager = player1.GetComponent<SPlayerManager>();
			playerManager.spawnPosition = respawns[0];
			playerManager.id = 0;
		}
		
		// PLAYER 2
		if(managerObjects[1] != null)
		{
			GameObject player2 = Instantiate(managerObjects[1], spawns[1].position ,Quaternion.identity);
			player2.name = managerObjects[1].name;
			players.Add(player2);
			SPlayerManager playerManager = player2.GetComponent<SPlayerManager>();
			playerManager.spawnPosition = respawns[1];
			playerManager.id = 1;

			
		}
		
		// PLAYER 3
		// if(managerObjects[2] != null)
		// {
		// 	GameObject player3 = Instantiate(managerObjects[2], spawns[2].position ,Quaternion.identity);
		// 	player3.name = managerObjects[2].name;
		// 	PlayerManager playerManager = player3.GetComponent<PlayerManager>();
		// 	playerManager.spawnPosition = respawns[2];
		
		// }
		
		// PLAYER 4
		// if(managerObjects[3] != null)
		// {
		// 	GameObject player4 = Instantiate(managerObjects[3], spawns[3].position ,Quaternion.identity);
		// 	player4.name = managerObjects[3].name;
	 	// 	PlayerManager playerManager = player4.GetComponent<PlayerManager>();
		// 	playerManager.spawnPosition = respawns[3];
		
		// }

		

	}

	// When there is one more player remaining, end the game.
	private void Update() 
	{
		if(players.Count == 1)
		{
			SceneManager.LoadScene("SEndResult");
		}
	}

	

}
