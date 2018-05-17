using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SceneChooser : EditorWindow
{
	public string path = "Assets/Scenes";
	

	[MenuItem("Tools/ChooseScenes")]
	public static void ChooseScenes()
	{
		SceneChooser inst = GetWindow<SceneChooser>();		
	}

	private void OnGUI() 
	{
		foreach(string file in System.IO.Directory.GetFiles(path).Where(s => s.EndsWith(".unity")))
		{
			Debug.Log(file);
		}
	}




}
