using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthIndicators : MonoBehaviour 
{
	private GameManager instance;
	public List<GameObject> listy;

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
		listy = GameManager.instance.players;

		if(listy[0] != null)
		{
			healthFirst = listy[0].GetComponent<PlayerHealth>();
			livesFirst = listy[0].GetComponent<PlayerManager>();
		}

		if(listy[1] != null)
		{
			healthSecond = listy[1].GetComponent<PlayerHealth>();
			livesSecond = listy[1].GetComponent<PlayerManager>();
		}

		// if(listy[2] != null)
		// {
		// 	healthThird = listy[2].GetComponent<PlayerHealth>();
		// 	livesThird = listy[2].GetComponent<PlayerManager>();
		// }

		// if(listy[3] != null)
		// {
		// 	healthFourth = listy[3].GetComponent<PlayerHealth>();
		// 	livesFourth = listy[3].GetComponent<PlayerManager>();
		// }
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(listy[0] != null)
		{
			player1Health.text = healthFirst.health.ToString() + "%";
			player1Lives.text = "Lives: "+ livesFirst.lives.ToString();
		}

		if(listy[1] != null)
		{
			player2Health.text = healthSecond.health.ToString() + "%";
			player2Lives.text = "Lives: "+ livesSecond.lives.ToString();	
		}
		
		// if(listy[2] != null)
		// {
		// 	player3Health.text = healthThird.health.ToString() + "%";
		// 	player3Lives.text = "Lives: "+ livesThird.lives.ToString();	
		// }


		// if(listy[3] != null)
		// {
		// 	player4Health.text = healthFourth.health.ToString() + "%";
		// 	player4Lives.text = "Lives: "+ livesFourth.lives.ToString();		
		// }

	}
}
