using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthIndicator : MonoBehaviour 
{
	public Text text;
	private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () 
	{
		playerHealth = GetComponent<PlayerHealth>();
		text.text = "0%";
	}
	
	// Update is called once per frame
	void Update () 
	{
		text.text = playerHealth.health.ToString() + "%";		
	}
}
