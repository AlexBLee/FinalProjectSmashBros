using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// Singleton to bring things between scenes.

	public static GameManager instance;
	public List<GameObject> players;
	public bool ready;

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
	private void Update() {
		Debug.Log(instance.ready);
	}
}
