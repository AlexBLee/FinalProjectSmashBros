using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// Singleton to bring things between scenes.

	public static GameManager instance;
	public List<GameObject> players;
	public bool ready;
	
	public int p1Number;
	public int p2Number;

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
