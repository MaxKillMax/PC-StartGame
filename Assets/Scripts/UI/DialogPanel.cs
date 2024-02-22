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

        public void Init(DialogNode dialog, Action<DialogVariant> onCompleted)
        {
            StopAllCoroutines();
            StartCoroutine(WaitForInit(dialog, onCompleted));
        }

        private IEnumerator WaitForInit(DialogNode dialog, Action<DialogVariant> onCompleted)
        {
            _answersPanel.Clear();
            yield return _text.StartTextSetting(dialog.Text);

            if (_text == null || _text.Canceled)
                yield break;

            yield return _answersPanel.Init(dialog.Variants, onCompleted);
        }
    }
}