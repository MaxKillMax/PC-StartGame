using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorOpenVideoFolder : MonoBehaviour
{
    [MenuItem("StartGame/Open Video Folder")]
    private static void OpenVideoFolder()
    {
        var itemPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)).Replace(@"/", @"\");
        ;
        System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
    }
}