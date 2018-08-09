using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpawner : MonoBehaviour {

	// Network object
	public GameObject network;

	// -----------------------------------------------------------------------------------------------------------------------//

	void Update () 
	{
		// If can't find NetworkManager then instantiate it.
		if(GameObject.Find("LobbyManager"))
		{
			Debug.Log("found");
		}
		else
		{
			Instantiate(network);
		}
		
	}
}
