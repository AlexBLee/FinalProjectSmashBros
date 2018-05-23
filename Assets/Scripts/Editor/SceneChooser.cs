using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SceneChooser : EditorWindow
{
	[System.Serializable]
	private struct Scene {
		public bool Build;
		public int index;
	}
	public string path = "Assets/Scenes";
	bool aa;
	public string jj;
	private static Dictionary<string,Scene> thingy = new Dictionary<string, Scene>();
	

	[MenuItem("Tools/ChooseScenes")]
	public static void ChooseScenes()
	{
		SceneChooser inst = GetWindow<SceneChooser>();
		
	}

	private void OnGUI() 
	{
		int index = 0;
		foreach(string file in System.IO.Directory.GetFiles(path).Where(s => s.EndsWith(".unity")))
		{
			if(!thingy.ContainsKey(file))
			{
				Scene a = new Scene();
				a.Build = true;
				thingy.Add(file,a);
			}
			Scene b = thingy[file];
			b.Build = EditorGUI.Toggle(new Rect(0,5+index,position.width+50,20),file,b.Build);
			b.index = EditorGUI.IntField(new Rect(15,5+index,position.width+50,20),b.index);
			thingy[file] = b;
			index+=20;
			Debug.Log(file);
		}

	}

	public static string[] GetScenesToBuild()
	{
		List<string> stringList = new List<string>();

		foreach(var pair in thingy.OrderBy(i => i.Value.index))
		{
			if(pair.Value.Build)
			{
				stringList.Add(pair.Key);
			}
		}
		

		return stringList.ToArray();
	}




}
