using SG.UI;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    private static GameLog Instance;

    [SerializeField] private TextPrinter _text;

    private void Awake()
    {
        Instance = this;
        _text.SetText(string.Empty);
    }

    public static void Log(string text) => _ = Instance._text.AddTextAsync($"- {text} \n");
}