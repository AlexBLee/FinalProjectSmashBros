using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthIndicator : MonoBehaviour 
{
	public Text text;
	public Text text2;
	private PlayerHealth playerHealth;
	private PlayerManager playerManager;

	// Use this for initialization
	void Start () 
	{
		playerHealth = GetComponent<PlayerHealth>();
		playerManager = GetComponent<PlayerManager>();
		text.text = "0%";

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		text.text = playerHealth.health.ToString() + "%";
		text2.text = "Lives: "+ playerManager.lives.ToString();	

	}
}
