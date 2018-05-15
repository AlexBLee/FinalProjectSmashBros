using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Results : MonoBehaviour 
{
	public Text[] textList;

	// Use this for initialization
	void Start () 
	{
		textList[0].text = "Name" + GameManager.instance.players[0].name;
		textList[1].text = " -- ";
		textList[2].text = "KOs: ";
		textList[3].text = "Falls: ";
		
	}
	
}
