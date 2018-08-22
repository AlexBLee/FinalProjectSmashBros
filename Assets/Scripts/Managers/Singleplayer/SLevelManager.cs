using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class SLevelManager : MonoBehaviour 
{
	// List that stays active across character select/game/end result screen
	private List<GameObject> managerObjects;

	// List for things in level.
	public List<GameObject> players;

	// Where the player initially spawns
	public Transform[] spawns;

	// Where the player will respawn after death
	public Transform[] respawns;

	// Display the Game over text
	public GameObject gameOverText;

	// For game over sound
	public AudioSource source;
	public AudioClip audio;

	// Display the countdown text
	public TextMeshProUGUI countdownText;

	// For time win condition
	public STimer timer;

	
	// -----------------------------------------------------------------------------------------------------------------------//	

	void Awake () 
	{
		// To stop the main menu music.
		Destroy(GameObject.Find("Music"));
		timer = FindObjectOfType<STimer>();

		managerObjects = SGameManager.instance.players;

		// If the players exist, instantiate them in their respective areas, add to the level list and set their spawn positions.

		// PLAYER 1
		if(managerObjects[0] != null)
		{
			GameObject player1 = Instantiate(managerObjects[0], spawns[0].position ,Quaternion.identity);
			player1.name = managerObjects[0].name;
			players.Add(player1);
			SPlayerManager playerManager = player1.GetComponent<SPlayerManager>();
			playerManager.spawnPosition = respawns[0];
			playerManager.id = 0;
		}
		
		// PLAYER 2
		if(managerObjects[1] != null)
		{
			GameObject player2 = Instantiate(managerObjects[1], spawns[1].position ,Quaternion.identity);
			player2.name = managerObjects[1].name;
			players.Add(player2);
			SPlayerManager playerManager = player2.GetComponent<SPlayerManager>();
			playerManager.spawnPosition = respawns[1];
			playerManager.id = 1;

			
		}
		
		StartCoroutine(StartLevel());

	}

	// When there is one more player remaining, end the game.
	private void Update() 
	{
		if(SGameManager.instance.gameModeNumber == 0)
		{
			if(players.Count == 1)
			{
				StartCoroutine(GameOverAndChangeScene());
			}
		}
		else if(SGameManager.instance.gameModeNumber == 1)
		{
			if(timer != null && timer.done)
			{
				StartCoroutine(GameOverAndChangeScene());
			}
		}
	}

	// Once game over has been reached, displayed victory text and load next scene.
	IEnumerator GameOverAndChangeScene()
	{
		gameOverText.SetActive(true);
		yield return new WaitForSecondsRealtime(4);
		SceneManager.LoadScene("SEndResult");
	}

	// Level start countdown
	IEnumerator StartLevel()
	{
		Time.timeScale = 0.0f;

		countdownText.text = "3";
		yield return new WaitForSecondsRealtime(1);
		countdownText.text = "2";
		yield return new WaitForSecondsRealtime(1);
		countdownText.text = "1";
		yield return new WaitForSecondsRealtime(1);
		countdownText.text = "START!";
		yield return new WaitForSecondsRealtime(1);
		countdownText.gameObject.SetActive(false);


		Time.timeScale = 1.0f;

	}

	

}
