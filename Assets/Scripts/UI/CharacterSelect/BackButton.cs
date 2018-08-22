using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackButton : MonoBehaviour 
{
	public static bool play;

	public void BackToMenu()
	{
		play = true;
		SceneManager.LoadScene("MainMenu");
	}
}
