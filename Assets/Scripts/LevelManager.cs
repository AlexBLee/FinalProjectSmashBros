using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	private GameManager instance;
	public List<GameObject> playerObjects;

	public Transform P1Spawn;
	public Transform P2Spawn;
	public Transform P3Spawn;
	public Transform P4Spawn;
	

	void Awake () 
	{
		playerObjects = GameManager.instance.players;

		if(playerObjects[0] != null)
		{
			GameObject player1 = Instantiate(playerObjects[0], P1Spawn.position ,Quaternion.identity);
			player1.name = playerObjects[0].name;
		}

		if(playerObjects[1] != null)
		{
			GameObject player2 = Instantiate(playerObjects[1], P2Spawn.position ,Quaternion.identity);
			player2.name = playerObjects[1].name;
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
