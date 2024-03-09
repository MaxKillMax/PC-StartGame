#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEngine;

public class RecorderLauncher : MonoBehaviour
{
    private RecorderController _recorderController;
    private MovieRecorderSettings _recorderSettings = null;

    void OnEnable()
    {
        EditorApplication.LockReloadAssemblies();

        DontDestroyOnLoad(this);

        Initialize();
    }

    private void Initialize()
    {
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "video" + DateTime.Now);
        var removePath = filePath + ".mp4";

        if (File.Exists(removePath))
            File.Delete(removePath);

        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        _recorderController = new RecorderController(controllerSettings);

        _recorderSettings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        _recorderSettings.name = "My Video Recorder";
        _recorderSettings.Enabled = true;

        _recorderSettings.OutputFormat = MovieRecorderSettings.VideoRecorderOutputFormat.MP4;
        _recorderSettings.VideoBitRateMode = VideoBitrateMode.High;

        _recorderSettings.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = 1920,
            OutputHeight = 1080
        };

        _recorderSettings.OutputFile = filePath;

        controllerSettings.AddRecorderSettings(_recorderSettings);
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRate = 60.0f;

        RecorderOptions.VerboseMode = false;
        _recorderController.PrepareRecording();
        _recorderController.StartRecording();
    }

    public void OnDestroy()
    {
        _recorderController.StopRecording();
    }
}
#endif