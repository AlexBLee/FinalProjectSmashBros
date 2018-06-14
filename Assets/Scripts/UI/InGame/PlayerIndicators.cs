using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerIndicators : MonoBehaviour 
{
	private List<GameObject> players = new List<GameObject>(4);

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

		for(int i = 0; i < players.Count; i++)
		{
			if(players.Count > i)
			{
				playerHealths[i] = players[i].GetComponent<PlayerHealth>();
				playerManagers[i] = players[i].GetComponent<PlayerManager>();
				playerHealthDisplays[i].gameObject.SetActive(true);
				playerLivesKillDisplay[i].gameObject.SetActive(true);
			}
			
		}

		#region non-looped

		// if(players[0] != null && players.Count > 0)
		// {
		// 	playerHealths[0] = players[0].GetComponent<PlayerHealth>();
		// 	playerManagers[0] = players[0].GetComponent<PlayerManager>();
		// 	playerHealthDisplays[0].gameObject.SetActive(true);
		// 	playerLivesKillDisplay[0].gameObject.SetActive(true);

		// }

		// if(players[1] != null && players.Count > 1)
		// {
		// 	playerHealths[1] = players[1].GetComponent<PlayerHealth>();
		// 	playerManagers[1] = players[1].GetComponent<PlayerManager>();
		// 	playerHealthDisplays[1].gameObject.SetActive(true);
		// 	playerLivesKillDisplay[1].gameObject.SetActive(true);
		// }

		// if(players.Count > 2)
		// {
		// 	playerHealths[2] = players[2].GetComponent<PlayerHealth>();
		// 	playerManagers[2] = players[2].GetComponent<PlayerManager>();
		// 	playerHealthDisplays[2].gameObject.SetActive(true);
		// 	playerLivesKillDisplay[2].gameObject.SetActive(true);
		// }

		// if(players.Count > 3)
		// {
		// 	playerHealths[3] = players[3].GetComponent<PlayerHealth>();
		// 	playerManagers[3] = players[3].GetComponent<PlayerManager>();
		// 	playerHealthDisplays[3].gameObject.SetActive(true);
		// 	playerLivesKillDisplay[3].gameObject.SetActive(true);
		// }

		#endregion non-looped
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		for(int i = 0; i < players.Count; i++)
		{
			if(players.Count > i)
			{
				playerHealthDisplays[i].text = playerHealths[i].health.ToString() + "%";


				if(GameManager.instance.gameModeNumber == 0)
				{
					playerLivesKillDisplay[i].text = "Lives: "+ playerManagers[i].lives.ToString();
				}

				if(GameManager.instance.gameModeNumber == 1)
				{
					playerLivesKillDisplay[i].text = "Kills: "+ playerManagers[i].kills.ToString();
				}
			}
			
		}

		#region update-non-looped
		// if(players[0] != null && players.Count > 0)
		// {
		// 	playerHealthDisplays[0].text = playerHealths[0].health.ToString() + "%";


		// 	if(GameManager.instance.gameModeNumber == 0)
		// 	{
		// 		playerLivesKillDisplay[0].text = "Lives: "+ playerManagers[0].lives.ToString();
		// 	}

		// 	if(GameManager.instance.gameModeNumber == 1)
		// 	{
		// 		playerLivesKillDisplay[0].text = "Kills: "+ playerManagers[0].kills.ToString();
		// 	}
		// }

		// if(players[1] != null && players.Count > 1)
		// {
		// 	playerHealthDisplays[1].text = playerHealths[1].health.ToString() + "%";
			
		// 	if(GameManager.instance.gameModeNumber == 0)
		// 	{
		// 		playerLivesKillDisplay[1].text = "Lives: "+ playerManagers[1].lives.ToString();
		// 	}

		// 	if(GameManager.instance.gameModeNumber == 1)
		// 	{
		// 		playerLivesKillDisplay[1].text = "Kills: "+ playerManagers[1].kills.ToString();
		// 	}
		// }
		
		// if(players.Count > 2)
		// {
		// 	playerHealthDisplays[2].text = playerHealths[2].health.ToString() + "%";
			
		// 	if(GameManager.instance.gameModeNumber == 0)
		// 	{
		// 		playerLivesKillDisplay[2].text = "Lives: "+ playerManagers[2].lives.ToString();
		// 	}

		// 	if(GameManager.instance.gameModeNumber == 1)
		// 	{
		// 		playerLivesKillDisplay[2].text = "Kills: "+ playerManagers[2].kills.ToString();
		// 	}
		// }


		// if(players.Count > 3)
		// {
		// 	playerHealthDisplays[3].text = playerHealths[3].health.ToString() + "%";
			
		// 	if(GameManager.instance.gameModeNumber == 0)
		// 	{
		// 		playerLivesKillDisplay[3].text = "Lives: "+ playerManagers[3].lives.ToString();
		// 	}

		// 	if(GameManager.instance.gameModeNumber == 1)
		// 	{
		// 		playerLivesKillDisplay[3].text = "Kills: "+ playerManagers[3].kills.ToString();
		// 	}
		// }
		#endregion update-non-looped


	}
}
