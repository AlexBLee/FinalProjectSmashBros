using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	private GameManager instance;
	public List<GameObject> managerObjects;
	public List<GameObject> players;

	public Transform P1Spawn;
	public Transform P2Spawn;
	public Transform P3Spawn;
	public Transform P4Spawn;
	

	void Awake () 
	{
		managerObjects = GameManager.instance.players;

		if(managerObjects[0] != null)
		{
			GameObject player1 = Instantiate(managerObjects[0], P1Spawn.position ,Quaternion.identity);
			player1.name = managerObjects[0].name;
			players.Add(player1);
		}

		if(managerObjects[1] != null)
		{
			GameObject player2 = Instantiate(managerObjects[1], P2Spawn.position ,Quaternion.identity);
			player2.name = managerObjects[1].name;
			players.Add(player2);
		}

		// if(playerObjects[2] != null)
		// {
		// 	GameObject player3 = Instantiate(playerObjects[2], P3Spawn.position ,Quaternion.identity);
		// 	player3.name = playerObjects[2].name;
		// }

		// if(playerObjects[3] != null)
		// {
		// 	GameObject player4 = Instantiate(playerObjects[3], P4Spawn.position ,Quaternion.identity);
		// 	player4.name = playerObjects[3].name;
		// }

	}
	

}
