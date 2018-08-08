using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpawner : MonoBehaviour {

	public GameObject network;

	// Update is called once per frame
	void Update () {
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
