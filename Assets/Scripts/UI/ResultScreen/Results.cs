using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Results : NetworkBehaviour 
{
	// To display the character
	public List<GameObject> characters;
	public List<Transform> spawnPosition;

	// Text
	public TextMeshProUGUI[] textList;

	// For finding the right positions for the players.
	public int index;

    // --------------------------------------------------------------------------------------------------------- //    

	// Show the name/kill/deaths.
	// NOTE: currently works for only two players. will likely have to be refactored to work with more if needed.
	void Start () 
	{
		textList[0].text = GameManager.instance.placeList[index].name;
		textList[1].text = "KOs: " + GameManager.instance.placeList[index].kills;
		textList[2].text = "Falls: " + GameManager.instance.placeList[index].deaths;

		if(GameManager.instance.placeList[index].name == "CatFighter")
		{
			Instantiate(characters[0], spawnPosition[index].position, Quaternion.identity);
		}
		else if(GameManager.instance.placeList[index].name == "DogFighter")
		{
			Instantiate(characters[1], spawnPosition[index].position, Quaternion.identity);
		}
		
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			GameManager.instance.ResetValues();
			NetworkManager.singleton.ServerChangeScene("CharacterSelect");

		}

		#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
				GameManager.instance.ResetValues();
				NetworkManager.singleton.ServerChangeScene("CharacterSelect");
            }
        #endif
		
	}
}
