using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

using UnityEngine.UI;
using UniRx;

public class NetworkCustom : NetworkManager
{
	public Transform spawnPosition;
	public int curPlayer;
	
	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("Player Connected!");

		IntegerMessage msg = new IntegerMessage(curPlayer);
	}

	 // Server
     public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader ) 
	 { 
         // Read client message and receive index
         if (extraMessageReader != null) 
		 {
             var stream = extraMessageReader.ReadMessage<IntegerMessage> ();
             curPlayer = stream.value;
         }
         //Select the prefab from the spawnable objects list
         var playerPrefab = spawnPrefabs[curPlayer];       
  
         // Create player object with prefab
         var player = Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity) as GameObject;        
         
         // Add player object for connection
         NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
     }
 }
	

	
