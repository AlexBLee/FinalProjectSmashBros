using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SResults : MonoBehaviour 
{
	// Text
	public Text[] textList;

	// For finding the right positions for the players.
	public int index;

    // --------------------------------------------------------------------------------------------------------- //    

	// Show the name/kill/deaths.
	// NOTE: currently works for only two players. will likely have to be refactored to work with more if needed.
	void Start () 
	{
		textList[0].text = SGameManager.instance.players[index].name;
		textList[1].text = "KOs: " + (index == 0 ? SGameManager.instance.p1Kills : SGameManager.instance.p2Kills);
		textList[2].text = "Falls: " + (index == 0 ? SGameManager.instance.p1Deaths : SGameManager.instance.p2Deaths);
		
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene("SCharacterSelect");	
		}
	}
}
