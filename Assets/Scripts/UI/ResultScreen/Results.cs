using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Results : MonoBehaviour 
{
	public Text[] textList;
	public int index;

	// Use this for initialization
	void Start () 
	{
		textList[0].text = GameManager.instance.players[index].name;
		textList[1].text = "KOs: " + (index == 0 ? GameManager.instance.p1Kills : GameManager.instance.p2Kills);
		textList[2].text = "Falls: " + (index == 0 ? GameManager.instance.p1Deaths : GameManager.instance.p2Deaths);
		
	}
	
}
