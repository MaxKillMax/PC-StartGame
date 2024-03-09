using System;
using System.Collections;
using SG.Dialogs;
using UnityEngine;

namespace SG.UI
{
    public class DialogPanel : MonoBehaviour
    {
        [SerializeField] private TextPrinter _text;
        [SerializeField] private AnswersPanel _answersPanel;

        public void Clear()
        {
            _text.SetText(string.Empty);
            _answersPanel.Clear();
        }

        public Coroutine Init(DialogNode dialog, Action<DialogVariant> onVariantCompleted = null)
        {
            StopAllCoroutines();
            return StartCoroutine(WaitForInit(dialog, onVariantCompleted));
        }

        private IEnumerator WaitForInit(DialogNode dialog, Action<DialogVariant> onVariantCompleted)
        {
            _answersPanel.Clear();
            yield return _text.StartTextSetting(dialog.Text);

            if (_text == null || _text.Canceled)
                yield break;

            yield return _answersPanel.Init(dialog.Variants, onVariantCompleted);
        }
    }
}