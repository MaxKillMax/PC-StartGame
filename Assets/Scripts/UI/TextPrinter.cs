using System.Threading;
using System.Threading.Tasks;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace SG.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextPrinter : MonoBehaviour
    {
        [SerializeField, ReadOnly] private TMP_Text _text;
        [SerializeField, ReadOnly] private int _printCharDelayInMilliseconds;
        [SerializeField] private float _printCharDelay = 0.02f;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _task;

        private string _result;

        public bool Canceled { get; private set; }
        public string Text => _text.text;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_text)
                _text = GetComponent<TMP_Text>();

            _printCharDelayInMilliseconds = (int)(_printCharDelay * 1000);
        }
#endif

        public void SetText(string value)
        {
            _text.text = value;
        }

        public void Cancel()
        {
            Canceled = true;
            _cancellationTokenSource?.Cancel();
        }

        public async Task AddTextAsync(string value)
        {
            await PlayAnimationTaskAsync(value, false);
        }

        public async Task SetTextAsync(string value)
        {
            if (_text.text == value)
                return;

            await PlayAnimationTaskAsync(value, true);
        }

        private async Task PlayAnimationTaskAsync(string value, bool resetText)
        {
            Canceled = false;

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                await _task;
            }

            _cancellationTokenSource = new();

            _text.text = _result;

            if (resetText)
                _text.text = string.Empty;

            _result = _text.text + value;

            _task = PlayAnimationAsync(value, _cancellationTokenSource.Token);
            await _task;

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;

            _text.text = _result;
        }

        private async Task PlayAnimationAsync(string value, CancellationToken token)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (_text == null)
                    return;

                _text.text += value[i];

                try
                {
                    await Task.Delay(_printCharDelayInMilliseconds, token);
                }
                catch
                {
                    return;
                }
            }
        }
    }
}