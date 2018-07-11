using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelManager1 : NetworkBehaviour 
{
	public GameObject[] objectToSpawn;
	public GameObject go;
	
	void Start () 
	{
		CmdSpawnUnits();
		SendMessage();
	}
	

	[Command]
	void CmdSpawnUnits()
	{
		int n = GameManager.instance.playerNumber;
		Vector2 spawn = GameManager.instance.spawn;

		go = Instantiate(objectToSpawn[n], spawn, Quaternion.identity);
		NetworkServer.Spawn(go);

		++GameManager.instance.playerNumber;
		GameManager.instance.spawn.x += 1.2f;
	}

	public void SendMessage()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		Debug.LogError("i am local!");
	}

}
