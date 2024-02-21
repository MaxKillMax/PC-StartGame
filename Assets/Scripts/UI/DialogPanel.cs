using System;
using SG.Dialogs;
using UnityEngine;

namespace SG.UI
{
    public class DialogPanel : MonoBehaviour
    {
        [SerializeField] private TextPrinter _text;
        [SerializeField] private AnswersPanel _answersPanel;

        public async void InitAsync(DialogNode dialog, Action<DialogVariant> onCompleted)
        {
            _answersPanel.Clear();

            _text.Cancel();
            await _text.SetTextAsync(dialog.Text);

            if (_text == null || _text.Canceled)
                return;

            _answersPanel.InitAsync(dialog.Variants, onCompleted);
        }
    }
}