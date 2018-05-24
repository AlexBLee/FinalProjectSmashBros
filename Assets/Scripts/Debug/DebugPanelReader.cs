using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugPanelReader : MonoBehaviour 
{
	public GameObject panel;
	public GameObject player;

	public InputField input;
	public InputField input2;
	public InputField input3;
	
	private int lives;
	private int moveSpeed;
	private int jumpVelocity;

	public void EnablePanel()
	{
		if(panel.activeSelf)
		{
			Debug.Log("!");
			panel.SetActive(false);
		}
		else if(!panel.activeSelf)
		{
			panel.SetActive(panel);
		}

	}

	public void GetFieldsOfObject()
	{
		lives = int.Parse(input.text);
		moveSpeed = int.Parse(input2.text);
		jumpVelocity = int.Parse(input3.text);
		
		if(input.text != null)
		{
			PropertyReader.SetField(typeof(PlayerManager), "lives", lives);
		}

		if(input2.text != null)
		{
			PropertyReader.SetField(typeof(PlayerMovement), "maxSpeed", moveSpeed);
		}
		
		if(input3.text != null)
		{
			PropertyReader.SetField(typeof(PlayerMovement), "jumpVelocity", jumpVelocity);
		}
		
	}
	
	
}
