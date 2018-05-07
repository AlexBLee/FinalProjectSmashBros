using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	public List<GameObject> players;

	public bool player1;
	public bool player2;
	public bool player3;
	public bool player4;
	

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

	}
}
