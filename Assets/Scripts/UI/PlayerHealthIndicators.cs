using System.Collections;
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

	public Text health;
	public Text health2;
	public Text health3;
	public Text health4;

	public Text lives;
	public Text lives2;
	public Text lives3;
	public Text lives4;
	
	

	// Use this for initialization
	void Start () 
	{
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

		if(players[2] != null)
		{
			healthThird = players[2].GetComponent<PlayerHealth>();
			livesThird = players[2].GetComponent<PlayerManager>();
		}

		if(players[3] != null)
		{
			healthFourth = players[3].GetComponent<PlayerHealth>();
			livesFourth = players[3].GetComponent<PlayerManager>();
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(players[0] != null)
		{
			health.text = healthFirst.health.ToString() + "%";
			lives.text = "Lives: "+ livesFirst.lives.ToString();
		}

		if(players[1] != null)
		{
			health2.text = healthSecond.health.ToString() + "%";
			lives2.text = "Lives: "+ livesSecond.lives.ToString();	
		}
		
		if(players[2] != null)
		{
			health3.text = healthThird.health.ToString() + "%";
			lives3.text = "Lives: "+ livesThird.lives.ToString();	
		}


		if(players[3] != null)
		{
			health4.text = healthFourth.health.ToString() + "%";
			lives4.text = "Lives: "+ livesFourth.lives.ToString();		
		}

	}
}
