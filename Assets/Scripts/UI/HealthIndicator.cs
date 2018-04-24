using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthIndicator : MonoBehaviour 
{
	public Text text;
	public Text text2;
	private PlayerHealth playerHealth;
	private DogManager dogManager;
	private CatManager catManager;

	// Use this for initialization
	void Start () 
	{
		playerHealth = GetComponent<PlayerHealth>();
		text.text = "0%";

		if(gameObject.name == "CatFighter")
        {
			catManager = GetComponent<CatManager>();
        }

        if(gameObject.name == "DogFighter")
        {
			dogManager = GetComponent<DogManager>();
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		text.text = playerHealth.health.ToString() + "%";

		if(dogManager != null)
			text2.text = "Lives: " + dogManager.lives.ToString();	

		if(catManager != null)
			text2.text = "Lives: " + catManager.lives.ToString();	
	}
}
