using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SPlayerIndicators : MonoBehaviour 
{
    // --------------------------------------------------------------------------------------------------------- //    

	// Player list.
	private List<GameObject> players = new List<GameObject>(4);

	// Health/Manager get lists.
	private SPlayerHealth[] playerHealths = new SPlayerHealth[4];
	private SPlayerManager[] playerManagers = new SPlayerManager[4];

	
	// Text displays
	public TextMeshProUGUI[] playerHealthDisplays;
	public TextMeshProUGUI[] playerLivesKillDisplay;

    // --------------------------------------------------------------------------------------------------------- //    

	void Start () 
	{
		SLevelManager levelManager = FindObjectOfType<SLevelManager>();

		// Used to find out how many players are in the game.
		foreach(GameObject t in levelManager.players)
		{
			players.Add(t);
		}


		// Gets the health and manager components of the player and shows the health and lives at the bottom of the screen
		// according to the amount of players.
		for(int i = 0; i < players.Count; i++)
		{
			playerHealths[i] = players[i].GetComponent<SPlayerHealth>();
			playerManagers[i] = players[i].GetComponent<SPlayerManager>();
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

			if(SGameManager.instance.gameModeNumber == 0)
			{
				playerLivesKillDisplay[i].text = "Lives: "+ playerManagers[i].lives.ToString();
			}

			if(SGameManager.instance.gameModeNumber == 1)
			{
				playerLivesKillDisplay[i].text = "Kills: "+ playerManagers[i].kills.ToString();
			}
			
		}

	}
}
