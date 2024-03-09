using SG.UI;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLog : MonoBehaviour
{
    private static GameLog Instance;

    [SerializeField] private TextPrinter _text;

    private void Awake()
    {
        Assert.IsNull(Instance);

        Instance = this;
        _text.SetText(string.Empty);
    }

    public static void Log(string text) => _ = Instance._text.StartTextAdding($"- {text} \n");
}