using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerIndicators : NetworkBehaviour 
{
    // --------------------------------------------------------------------------------------------------------- //    

	// Player list.
	public List<GameObject> players = new List<GameObject>(4);

	// Health/Manager get lists.
	private PlayerHealth[] playerHealths = new PlayerHealth[4];
	private PlayerManager[] playerManagers = new PlayerManager[4];

	// Text displays
	public TextMeshProUGUI[] playerHealthDisplays;
	public TextMeshProUGUI[] playerLivesKillDisplay;

    // --------------------------------------------------------------------------------------------------------- //    

	IEnumerator Start () 
	{
		// The delay is here for the online mode as the stuff below executes faster than the server can finish loading everything.
		yield return new WaitForSeconds(0.1f);

		
		// Used to find out how many players are in the game.
		LevelManager levelManager = FindObjectOfType<LevelManager>();

		foreach(LevelManager.ID t in levelManager.syncPlayers)
		{
			players.Add(ClientScene.FindLocalObject(t.netID));
		}


		// Gets the health and manager components of the player and shows the health and lives at the bottom of the screen
		// according to the amount of players.
		for(int i = 0; i < players.Count; i++)
		{
			playerHealths[i] = players[i].GetComponent<PlayerHealth>();
			playerManagers[i] = players[i].GetComponent<PlayerManager>();
			playerHealthDisplays[i].gameObject.SetActive(true);
			playerLivesKillDisplay[i].gameObject.SetActive(true);
			
		}
	}
	
	void Update () 
	{
		// Show the health percentages and amount of lives for the player.
		for(int i = 0; i < players.Count; i++)
		{
			playerHealthDisplays[i].text = playerHealths[i].health.ToString() + "%";

			if(GameManager.instance.gameModeNumber == 0)
			{
				playerLivesKillDisplay[i].text = "Lives: "+ playerManagers[i].lives.ToString();
			}

			if(GameManager.instance.gameModeNumber == 1)
			{
				playerLivesKillDisplay[i].text = "Kills: "+ playerManagers[i].stats.kills.ToString();
			}
			
		}

	}
}
