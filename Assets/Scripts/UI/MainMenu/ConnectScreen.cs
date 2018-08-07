using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ConnectScreen : MonoBehaviour 
{
	public Button joinButton;
	public Button hostButton;

	public TMP_InputField inputField;
	public static string ip;

	public static bool host;

	// Use this for initialization
	void Start () 
	{
		if(joinButton != null)
			joinButton.onClick.AddListener(Join);

		if(hostButton != null)
			hostButton.onClick.AddListener(Host);

	}

	void Join()
	{
		ip = inputField.text;
        SceneManager.LoadScene("CharacterSelect");
		host = false;
	}

	void Host()
	{
		ip = inputField.text;
        SceneManager.LoadScene("CharacterSelect");
		host = true;
	}


	
	
}
