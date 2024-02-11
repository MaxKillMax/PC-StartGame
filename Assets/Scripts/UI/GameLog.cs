using TMPro;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    private static GameLog Instance;

    [SerializeField] private TMP_Text _logText;

    private void Awake()
    {
        Instance = this;
        _logText.text = string.Empty;
    }

    public static void Log(string text)
    {
        Instance._logText.text += $"- {text} \n";
    }
}