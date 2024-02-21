using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SG.Dialogs;
using UnityEngine;

namespace SG.UI
{
    public class AnswersPanel : MonoBehaviour
    {
        [SerializeField] private AnswerButton _answerButtonPrefab;

        private CancellationTokenSource _cancellationTokenSource;
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

        public async void InitAsync(List<DialogVariant> variants, Action<DialogVariant> onCompleted)
        {
            Clear();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new();

            foreach (DialogVariant variant in variants)
            {
                if (_transform == null)
                    return;

                await InitVariantAsync(variant, onCompleted, _cancellationTokenSource.Token);
            }

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        private async Task InitVariantAsync(DialogVariant variant, Action<DialogVariant> onCompleted, CancellationToken token)
        {
            AnswerButton button = Instantiate(_answerButtonPrefab, _transform);

            token.Register(button.CancelInit);
            bool inited = await button.InitAsync(AnswerButtons.Count + 1, variant, () => onCompleted?.Invoke(variant));

            if (token.IsCancellationRequested)
                return;

            if (inited)
                AnswerButtons.Add(button);
            else
                Destroy(button.gameObject);
        }

        public void Clear()
        {
            foreach (AnswerButton button in AnswerButtons)
                Destroy(button.gameObject);

            AnswerButtons.Clear();
        }
    }
}
