using System;
using System.Collections.Generic;
using SG.Dialogs;
using UnityEngine;

namespace SG.UI
{
    public class AnswersPanel : MonoBehaviour
    {
        [SerializeField] private AnswerButton _answerButtonPrefab;

        public List<AnswerButton> AnswerButtons { get; } = new();

        public void Init(List<DialogVariant> variants, Action<DialogVariant> onCompleted)
        {
            if (AnswerButtons.Count > 0)
                Clear();

            foreach (DialogVariant variant in variants)
                Initialize(variant, onCompleted);
        }

        private void Initialize(DialogVariant variant, Action<DialogVariant> onCompleted)
        {
            AnswerButton button = Instantiate(_answerButtonPrefab, transform);
            bool inited = button.Init(AnswerButtons.Count + 1, variant, () => onCompleted?.Invoke(variant));

            if (inited)
                AnswerButtons.Add(button);
            else
                Destroy(button.gameObject);
        }

        private void Clear()
        {
            foreach (AnswerButton button in AnswerButtons)
                Destroy(button.gameObject);

            AnswerButtons.Clear();
        }
    }
}
