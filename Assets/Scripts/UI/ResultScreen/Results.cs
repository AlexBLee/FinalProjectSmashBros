using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Results : MonoBehaviour 
{
	// Text
	public TextMeshProUGUI[] textList;

	// For finding the right positions for the players.
	public int index;

    // --------------------------------------------------------------------------------------------------------- //    

	// Show the name/kill/deaths.
	// NOTE: currently works for only two players. will likely have to be refactored to work with more if needed.
	void Start () 
	{
		textList[0].text = GameManager.instance.players[index].name;
		textList[1].text = "KOs: " + (index == 0 ? GameManager.instance.p1Kills : GameManager.instance.p2Kills);
		textList[2].text = "Falls: " + (index == 0 ? GameManager.instance.p1Deaths : GameManager.instance.p2Deaths);
		
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene("CharacterSelect");	
		}
	}
}
