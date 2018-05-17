using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildVersionMenu
{
    private static string BuildVersionFilePath = "Assets/Resources/Version.txt";

    #region MenuMethods
    [MenuItem("Build/Version/MajorVersion++")]
    public static void IncrementMajorVersion()
    {
        string curVersion = PlayerSettings.bundleVersion; // "1.2.6.9278"
        int major = Convert.ToInt32(curVersion.Split('.')[0])+1;
        int minor = 0;
        int patch = 0;
        string newVersion = string.Format("{0}.{1}.{2}.{3}", major,minor,patch,GetCommitNumber());
        PlayerSettings.bundleVersion = newVersion;
    }

    [MenuItem("Build/Version/MinorVersion++")]
    public static void IncrementMinorVersion()
    {
        string curVersion = PlayerSettings.bundleVersion; // "1.2.6.9278"
        int major = Convert.ToInt32(curVersion.Split('.')[0]);
        int minor = Convert.ToInt32(curVersion.Split('.')[1])+1;
        int patch = 0;
        string newVersion = string.Format("{0}.{1}.{2}.{3}", major,minor,patch,GetCommitNumber());
        PlayerSettings.bundleVersion = newVersion;
    }

    [MenuItem("Build/Version/PatchVersion++")]
    public static void IncrementPatchVersion()
    {
        string curVersion = PlayerSettings.bundleVersion; // "1.2.6.9278"
        int major = Convert.ToInt32(curVersion.Split('.')[0]);
        int minor = Convert.ToInt32(curVersion.Split('.')[1]);
        int patch = Convert.ToInt32(curVersion.Split('.')[2])+1;
        
        string newVersion = string.Format("{0}.{1}.{2}.{3}", major,minor,patch,GetCommitNumber());
        PlayerSettings.bundleVersion = newVersion;        
    }

    [MenuItem("Build/Version/SyncCommitNumber")]
    public static void SyncCommitNumber()
    {
        string curVersion = PlayerSettings.bundleVersion; // "1.2.6.9278"
        int major = Convert.ToInt32(curVersion.Split('.')[0]);
        int minor = Convert.ToInt32(curVersion.Split('.')[1]);
        int patch = Convert.ToInt32(curVersion.Split('.')[2]);
        
        string newVersion = string.Format("{0}.{1}.{2}.{3}", major,minor,patch,GetCommitNumber());
        PlayerSettings.bundleVersion = newVersion; 
    }

    [MenuItem("Build/UpdateFile")]
    public static void SaveToFile()
    {
        string version = PlayerSettings.bundleVersion;

        StreamWriter writer = new StreamWriter(BuildVersionFilePath,false);
        writer.Write(version);
        writer.Close();
    }

    [MenuItem("Build/ResetVersion")]
    public static void ResetVersion()
    {
        StreamReader reader = new StreamReader(BuildVersionFilePath);
        string fileVersion = reader.ReadToEnd();
        reader.Close();
        if(fileVersion.Split('.').Length != 4)
        {
            UnityEngine.Debug.LogError("Version in file " + BuildVersionFilePath + " does not match accepted format.");
            return;
        }
        PlayerSettings.bundleVersion = fileVersion;
    }
    #endregion MenuMethods

    public static int GetCommitNumber()
    {
        Process p = new Process();
        p.StartInfo.FileName = "git";
        p.StartInfo.Arguments = "rev-list HEAD --count";
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.WorkingDirectory = Application.dataPath + "/..";
        p.StartInfo.UseShellExecute = false;
        p.Start();

        string output = p.StandardOutput.ReadToEnd();
        int commitNum = 0;
        if(int.TryParse(output,out commitNum))
        {
            UnityEngine.Debug.Log("Git commit number: " + commitNum);
        }
        else
        {
            UnityEngine.Debug.LogError("Git gud. Cannot parse the git commit number from string: " + output);
        }

        return commitNum;


    }

}
