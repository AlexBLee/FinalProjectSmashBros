using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
#pragma warning disable 0618


public class PreBuildMethods : IPreprocessBuild, IPostprocessBuild
{
    public int callbackOrder { get { return 0; } }
	
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        if(CustomBuildProcess.IsManualBuild) return;
		    BuildVersionMenu.IncrementPatchVersion();
		    BuildVersionMenu.SaveToFile();
    }

    public void OnPostprocessBuild(BuildTarget target, string path)
    {
        if(CustomBuildProcess.IsManualBuild) return;
		    FileUtil.ReplaceFile("Assets/Templates/Readme.txt", path + "/../Readme.txt");
    }

    [PostProcessBuild(0)]
    public static void OtherPostProcessBuild(BuildTarget target, string path)
    {
        Debug.Log("OtherPostProcessBuild");
    }

    [PostProcessBuild(1)]
    public static void SecondOtherPostProcessBuild(BuildTarget target, string path)
    {
        Debug.Log("SecondOtherPostProcessBuild");
    }
}

// public class PostBuildMethods : IPostprocessBuild
// {
//     public int callbackOrder { get { return 0; } }
//     public void OnPostprocessBuild(BuildTarget target, string path)
//     {
//         if(CustomBuildProcess.IsManualBuild) return;
// 		    FileUtil.ReplaceFile("Assets/Templates/Readme.txt", path + "/../Readme.txt");
//     }
// }
