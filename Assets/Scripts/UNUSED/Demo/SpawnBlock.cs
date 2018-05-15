using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnBlock : MonoBehaviour 
{
	public GameObject block;
	private Button button;
	private Vector2 spawn = new Vector2(0,0);

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(SpawnBlockAtPosition);
		
	}

	public void SpawnBlockAtPosition()
	{
		Instantiate(block,spawn,Quaternion.identity);
	}
}
