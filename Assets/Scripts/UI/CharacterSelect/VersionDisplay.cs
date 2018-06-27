using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VersionDisplay : MonoBehaviour 
{
	// Version display Text
	private Text text;

	// Display version.
	private void Start() 
	{
	   string curVersion = Application.version;
		
		text = GetComponent<Text>();
		text.text = curVersion;
	}

}
