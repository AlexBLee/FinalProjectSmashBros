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
    private GameObject player;
	
	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("Player Connected!");

		IntegerMessage msg = new IntegerMessage(curPlayer);
        ClientScene.AddPlayer(conn, 0, msg);

        
	}

	 //Server
     public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader ) 
	 { 
         Debug.Log("add player!");

         foreach(NetworkConnection conna in NetworkServer.connections)
         {
             Debug.Log(conna);
         }
         // Read client message and receive index
         if (extraMessageReader != null) 
		 {
             var stream = extraMessageReader.ReadMessage<IntegerMessage> ();
             curPlayer = stream.value;
         }

		Vector2 spawn = GameManager.instance.spawn;
        int n = GameManager.instance.playerNumber;

         //Select the prefab from the spawnable objects list
         var playerPrefab = spawnPrefabs[n];
  
         // Create player object with prefab
         var player = Instantiate(playerPrefab, spawn, Quaternion.identity) as GameObject;        
         
		GameManager.instance.spawn.x += 1.2f;
        GameManager.instance.playerNumber++;

         // Add player object for connection
         NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

     }

     public override void OnServerDisconnect(NetworkConnection conn)
     {
         GameManager.instance.spawn.x -= 1.2f;
         GameManager.instance.playerNumber--;

     }

     public override void OnServerSceneChanged(string sceneName)
     {

         if(sceneName == "Level1" || sceneName == "Level2" || sceneName == "Level3")
         {

            GameObject.Find("LevelManager").GetComponent<LevelManager>().CmdSpawnUnits();
         }


     }

     

     private void Update() {


     }
 }
	

	
