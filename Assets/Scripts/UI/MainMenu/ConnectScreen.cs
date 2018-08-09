using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ConnectScreen : MonoBehaviour 
{
	// Buttons
	public Button joinButton;
	public Button hostButton;

	// Input field for IP input field
	public TMP_InputField inputField;
	public static string ip;

	// To determine if host or not
	public static bool host;

    // --------------------------------------------------------------------------------------------------------- //    


	// Use this for initialization
	void Start () 
	{
		// Add functions to buttons
		if(joinButton != null)
			joinButton.onClick.AddListener(Join);

		if(hostButton != null)
			hostButton.onClick.AddListener(Host);

	}

	// Join the server.
	void Join()
	{
		ip = inputField.text;
        SceneManager.LoadScene("CharacterSelect");
		host = false;
	}

	// Host a server.
	void Host()
	{
		ip = inputField.text;
        SceneManager.LoadScene("CharacterSelect");
		host = true;
	}


	
	
}
