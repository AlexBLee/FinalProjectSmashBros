using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SResults : MonoBehaviour 
{
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
		
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene("SCharacterSelect");	
		}
	}
}
