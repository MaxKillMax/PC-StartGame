using System;
using System.Collections;
using System.Collections.Generic;
using SG.Dialogs;
using UnityEngine;

namespace SG.UI
{
    public class AnswersPanel : MonoBehaviour
    {
        [SerializeField] private AnswerButton _answerButtonPrefab;

        private Transform _transform;

        public List<AnswerButton> AnswerButtons { get; } = new();

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDestroy()
        {
            _transform = null;
        }

        public Coroutine Init(List<DialogVariant> variants, Action<DialogVariant> onVariantCompleted)
        {
            Clear();
            StopAllCoroutines();
            return StartCoroutine(WaitForInit(variants, onVariantCompleted));
        }

        private IEnumerator WaitForInit(List<DialogVariant> variants, Action<DialogVariant> onVariantCompleted)
        {
            foreach (DialogVariant variant in variants)
            {
                if (_transform == null)
                    yield break;

                yield return StartCoroutine(WaitForButtonInit(variant, onVariantCompleted));
            }
        }

        private IEnumerator WaitForButtonInit(DialogVariant variant, Action<DialogVariant> onVariantCompleted)
        {
            AnswerButton button = Instantiate(_answerButtonPrefab, _transform);
            yield return button.Init(AnswerButtons.Count + 1, variant, () => onVariantCompleted?.Invoke(variant));

            if (button.IsInited)
                AnswerButtons.Add(button);
            else
                Destroy(button.gameObject);

            yield break;
        }

        public void Clear()
        {
            foreach (AnswerButton button in AnswerButtons)
                Destroy(button.gameObject);

            AnswerButtons.Clear();
        }
    }
}
