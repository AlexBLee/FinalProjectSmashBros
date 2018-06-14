using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerIndicators : MonoBehaviour 
{
	public List<GameObject> players;

	private PlayerHealth[] playerHealths = new PlayerHealth[4];

	private PlayerManager[] playerManagers = new PlayerManager[4];

	public Text[] playerHealthDisplays;

	public Text[] playerLivesKillDisplay;
	

	// Use this for initialization
	void Start () 
	{
		LevelManager levelManager = FindObjectOfType<LevelManager>();

		foreach(GameObject t in levelManager.players)
		{
			players.Add(t);
		}

		// --------------------------------------------------------------------------

		if(players[0] != null)
		{
			playerHealths[0] = players[0].GetComponent<PlayerHealth>();
			playerManagers[0] = players[0].GetComponent<PlayerManager>();
		}

		if(players[1] != null)
		{
			playerHealths[1] = players[1].GetComponent<PlayerHealth>();
			playerManagers[1] = players[1].GetComponent<PlayerManager>();
		}

		// if(players[2] != null)
		// {
		// 	playerHealths[2] = players[2].GetComponent<PlayerHealth>();
		// 	playerManagers[2] = players[2].GetComponent<PlayerManager>();
		// }

		// if(players[3] != null)
		// {
		// 	playerHealths[3] = players[3].GetComponent<PlayerHealth>();
		// 	playerManagers[3] = players[3].GetComponent<PlayerManager>();
		// }
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(players[0] != null)
		{
			playerHealthDisplays[0].text = playerHealths[0].health.ToString() + "%";


			if(GameManager.instance.gameModeNumber == 0)
			{
				playerLivesKillDisplay[0].text = "Lives: "+ playerManagers[0].lives.ToString();
			}

			if(GameManager.instance.gameModeNumber == 1)
			{
				playerLivesKillDisplay[0].text = "Kills: "+ playerManagers[0].kills.ToString();
			}
		}

		if(players[1] != null)
		{
			playerHealthDisplays[1].text = playerHealths[1].health.ToString() + "%";
			
			if(GameManager.instance.gameModeNumber == 0)
			{
				playerLivesKillDisplay[1].text = "Lives: "+ playerManagers[1].lives.ToString();
			}

			if(GameManager.instance.gameModeNumber == 1)
			{
				playerLivesKillDisplay[1].text = "Kills: "+ playerManagers[1].kills.ToString();
			}
		}
		
		// if(players[2] != null)
		// {
		// 	playerHealthDisplays[2].text = playerHealths[2].health.ToString() + "%";
		// 	playerLivesKillDisplay[2].text = "Lives: "+ playerManagers[2].lives.ToString();	
		// }


		// if(players[3] != null)
		// {
		// 	playerHealthDisplays[3].text = playerHealths[3].health.ToString() + "%";
		// 	playerLivesKillDisplay[3].text = "Lives: "+ playerManagers[3].lives.ToString();		
		// }

	}
}
