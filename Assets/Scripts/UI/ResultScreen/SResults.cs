using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SResults : MonoBehaviour 
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
	void Start () 
	{
		textList[0].text = SGameManager.instance.placeList[index].name;
		textList[1].text = "KOs: " + SGameManager.instance.placeList[index].kills;
		textList[2].text = "Falls: " + SGameManager.instance.placeList[index].deaths;
		
		if(SGameManager.instance.placeList[index].name == "CatFighter")
		{
			Instantiate(characters[0], spawnPosition[index].position, Quaternion.identity);
		}
		else if(SGameManager.instance.placeList[index].name == "DogFighter")
		{
			Instantiate(characters[1], spawnPosition[index].position, Quaternion.identity);
		}
		
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			SGameManager.instance.ResetValues();
			SceneManager.LoadScene("SCharacterSelect");	
		}

		#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
				SGameManager.instance.ResetValues();
				SceneManager.LoadScene("SCharacterSelect");	
            }
        #endif
	}
}
