using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	// List that stays active across character select/game/end result screen
	public List<GameObject> managerObjects;

	// List for things in level.
	public List<GameObject> players;

	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;
	

	void Awake () 
	{
		managerObjects = GameManager.instance.players;

		// If the players exist, instantiate them in their respective areas, add to the level list and give them respawn areas.

		if(managerObjects[0] != null)
		{
			GameObject player1 = Instantiate(managerObjects[0], spawns[0].position ,Quaternion.identity);
			player1.name = managerObjects[0].name;
			players.Add(player1);
			PlayerManager playerManager = player1.GetComponent<PlayerManager>();
			playerManager.spawnPosition = respawns[0];
		}

		if(managerObjects[1] != null)
		{
			GameObject player2 = Instantiate(managerObjects[1], spawns[1].position ,Quaternion.identity);
			player2.name = managerObjects[1].name;
			players.Add(player2);
			PlayerManager playerManager = player2.GetComponent<PlayerManager>();
			playerManager.spawnPosition = respawns[1];
			
		}

		// if(managerObjects[2] != null)
		// {
		// 	GameObject player3 = Instantiate(managerObjects[2], spawns[2].position ,Quaternion.identity);
		// 	player3.name = managerObjects[2].name;
		// 	PlayerManager playerManager = player3.GetComponent<PlayerManager>();
		// 	playerManager.spawnPosition = respawns[2];
		
		// }

		// if(managerObjects[3] != null)
		// {
		// 	GameObject player4 = Instantiate(managerObjects[3], spawns[3].position ,Quaternion.identity);
		// 	player4.name = managerObjects[3].name;
	 	// 	PlayerManager playerManager = player4.GetComponent<PlayerManager>();
		// 	playerManager.spawnPosition = respawns[3];
		
		// }

	}
	

}
