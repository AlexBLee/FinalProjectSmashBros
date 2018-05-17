using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class VersionDisplay : MonoBehaviour 
{
	private Text text;

	private void Start() 
	{
	   string curVersion = PlayerSettings.bundleVersion;
		
		text = GetComponent<Text>();
		text.text = curVersion;
	}

}
