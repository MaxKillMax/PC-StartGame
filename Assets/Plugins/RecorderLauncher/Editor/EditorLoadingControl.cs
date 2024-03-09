using UnityEngine;
using UnityEditor;

[InitializeOnLoadAttribute]
public class EditorLoadingControl
{
    private static RecorderLauncher _recorderLauncher = null;

    static EditorLoadingControl()
    {
        EditorApplication.playModeStateChanged += CreateUnityRecorder;
    }

    ~EditorLoadingControl()
    {
        EditorApplication.playModeStateChanged -= CreateUnityRecorder;
    }

    private static void CreateUnityRecorder(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            _recorderLauncher = new GameObject("RecorderLauncher").AddComponent<RecorderLauncher>();
        }
        else if(state == PlayModeStateChange.ExitingPlayMode)
        {
            if(_recorderLauncher != null)
            {
                Object.Destroy(_recorderLauncher.gameObject);
            }
        }
    }   
}