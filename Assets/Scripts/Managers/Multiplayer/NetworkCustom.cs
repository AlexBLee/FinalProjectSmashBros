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

    private void Start() 
    {
        // Get the address from the input in the connect screen.
        networkAddress = ConnectScreen.ip;
        
        // If the player chose to host, start server, if they chose to join, connect to an IP.
        if(ConnectScreen.host)
        {
            StartServer();
        }
        else
        {
            StartClient();
        }
    }
	
    // Add player when they connect
	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("Player Connected!");

		IntegerMessage msg = new IntegerMessage(curPlayer);

        ClientScene.AddPlayer(conn,0,msg);
	}

	 //Server
     public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader ) 
	 { 
         Debug.Log("add player!");

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
         NetworkServer.DestroyPlayersForConnection(conn);
         GameManager.instance.spawn.x -= 1.2f;
         GameManager.instance.playerNumber--;

     }

     public override void OnStopServer()
     {
         GameManager.instance.spawn = new Vector2(-0.7f,0.55f);
         GameManager.instance.playerNumber = 2;
     }

     public override void OnServerSceneChanged(string sceneName)
     {
         if(sceneName == "CharacterSelect")
         {
            NetworkServer.Reset();

            for(int i = 0; i < GameManager.instance.players.Count; i++)
            {
                GameManager.instance.players[i] = null;
            }
            GameManager.instance.ready = false;
            GameManager.instance.spawn = new Vector2(-0.7f,0.55f);
            GameManager.instance.playerNumber = 2;

            NetworkTransport.RemoveHost(NetworkManager.singleton.client.connection.hostId);


            NetworkManager.singleton.client.Shutdown();
            NetworkManager.singleton.client = null;


            NetworkTransport.Shutdown();
            Destroy(gameObject);
         }

         if(sceneName == "Level1" || sceneName == "Level2" || sceneName == "Level3")
         {
            LevelManager lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            lvlManager.CmdSpawnUnits();
         }


     }

     public override void OnClientSceneChanged(NetworkConnection conn)
     {

     }


 }
	

	
