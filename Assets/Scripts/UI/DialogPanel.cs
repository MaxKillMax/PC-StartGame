using System;
using SG.Dialogs;
using TMPro;
using UnityEngine;

namespace SG.UI
{
    public class DialogPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private AnswersPanel _answersPanel;

        public void Init(DialogNode dialog, Action<DialogVariant> onCompleted)
        {
            _text.text = dialog.Text;
            _answersPanel.Init(dialog.Variants, onCompleted);
        }
    }
}