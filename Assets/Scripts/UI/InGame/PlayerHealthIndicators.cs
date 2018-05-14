﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthIndicators : MonoBehaviour 
{
	public List<GameObject> players;

	private PlayerHealth healthFirst;
	private PlayerHealth healthSecond;
	private PlayerHealth healthThird;
	private PlayerHealth healthFourth;

	private PlayerManager livesFirst;
	private PlayerManager livesSecond;
	private PlayerManager livesThird;
	private PlayerManager livesFourth;

	public Text player1Health;
	public Text player2Health;
	public Text player3Health;
	public Text player4Health;

	public Text player1Lives;
	public Text player2Lives;
	public Text player3Lives;
	public Text player4Lives;
	

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
			healthFirst = players[0].GetComponent<PlayerHealth>();
			livesFirst = players[0].GetComponent<PlayerManager>();
		}

		if(players[1] != null)
		{
			healthSecond = players[1].GetComponent<PlayerHealth>();
			livesSecond = players[1].GetComponent<PlayerManager>();
		}

		// if(players[2] != null)
		// {
		// 	healthThird = players[2].GetComponent<PlayerHealth>();
		// 	livesThird = players[2].GetComponent<PlayerManager>();
		// }

		// if(players[3] != null)
		// {
		// 	healthFourth = players[3].GetComponent<PlayerHealth>();
		// 	livesFourth = players[3].GetComponent<PlayerManager>();
		// }
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(players[0] != null)
		{
			player1Health.text = healthFirst.health.ToString() + "%";
			player1Lives.text = "Lives: "+ livesFirst.lives.ToString();
		}

		if(players[1] != null)
		{
			player2Health.text = healthSecond.health.ToString() + "%";
			player2Lives.text = "Lives: "+ livesSecond.lives.ToString();	
		}
		
		// if(players[2] != null)
		// {
		// 	player3Health.text = healthThird.health.ToString() + "%";
		// 	player3Lives.text = "Lives: "+ livesThird.lives.ToString();	
		// }


		// if(players[3] != null)
		// {
		// 	player4Health.text = healthFourth.health.ToString() + "%";
		// 	player4Lives.text = "Lives: "+ livesFourth.lives.ToString();		
		// }

	}
}