using System;
using System.IO;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEngine;

public class AddRecorderPackage : MonoBehaviour
{
    [MenuItem("StartGame/Add Recorder package")]
    private static void AddPackage()
    {
        Client.Add("com.unity.recorder");
    }
}
