using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class CustomBuildProcess
{
	private static string buildGameName = "/BuiltGame.exe";
	private static string readMeDestination = "Assets/Templates/Readme.txt";
	private static string readMe = "/Readme.txt";
	
	
	

	public static bool IsManualBuild = false;
	[MenuItem("Tools/Windows Build With Postprocess")]
	public static void BuildGame()
	{
		IsManualBuild = true;
		// Example PRE-process
		BuildVersionMenu.IncrementPatchVersion();
		BuildVersionMenu.SaveToFile();

		// Build settings
		string filepath = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "","");
		string[] scenes = new string[] { };

		
		// Build player
		BuildPipeline.BuildPlayer(SceneChooser.GetScenesToBuild(), filepath + buildGameName, BuildTarget.StandaloneWindows, BuildOptions.None);

		// Copy a file from the project folder to the build folder, alongside the built game
		// Example POST process
		FileUtil.ReplaceFile(readMeDestination, filepath + readMe);

		Process p = new Process();
		p.StartInfo.FileName = filepath + buildGameName;
		p.Start();
		IsManualBuild = false;
	}

}
