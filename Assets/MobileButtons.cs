using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButtons : EventTrigger 
{
	public GameObject player;

	// Use this for initialization
	void Start () 
	{

		#if UNITY_STANDALONE || UNITY_EDITOR
			gameObject.SetActive(false);
		#endif

		#if UNITY_ANDROID
			gameObject.SetActive(true);
		#endif
	}

	

	
	public override void OnPointerClick(PointerEventData eventData)
	{
		if(gameObject.name == "A")
		{
			if(player.gameObject.name == "DogFighter")
			{
				player.GetComponent<DogControls>().AButton();
			}

			if(player.gameObject.name == "CatFighter")
			{
				player.GetComponent<CatControls>().AButton();
			}
		}

		if(gameObject.name == "B")
		{
			if(player.gameObject.name == "DogFighter")
			{
				player.GetComponent<DogControls>().BButton();
			}

			if(player.gameObject.name == "CatFighter")
			{
				player.GetComponent<CatControls>().BButton();
			}
		}

		if(gameObject.name == "C")
		{
			if(player.gameObject.name == "DogFighter")
			{
				player.GetComponent<DogControls>().CButton();
			}

			if(player.gameObject.name == "CatFighter")
			{
				player.GetComponent<CatControls>().CButton();
			}
		}

		if(gameObject.name == "D")
		{
			if(player.gameObject.name == "DogFighter")
			{
				player.GetComponent<DogControls>().DButton();
			}

			if(player.gameObject.name == "CatFighter")
			{
				player.GetComponent<CatControls>().DButton();
			}

		}
	}

	

}
