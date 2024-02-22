using System.Collections;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace SG.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextPrinter : MonoBehaviour
    {
        [SerializeField, ReadOnly] private TMP_Text _text;
        [SerializeField] private float _printCharDelay = 0.02f;

        private Coroutine _coroutine;
        private string _result;

        public bool Canceled { get; private set; }
        public string Text => _text.text;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_text)
                _text = GetComponent<TMP_Text>();
        }
#endif

        public void SetText(string value)
        {
            _text.text = value;
        }

        private void Cancel()
        {
            if (_coroutine == null)
                return;

            Canceled = true;
            StopCoroutine(_coroutine);

            _coroutine = null;
            _text.text = _result;
        }

        public Coroutine StartTextAdding(string value) => StartCoroutine(WaitForSingleAnimationPlaying(value, false));

        public Coroutine StartTextSetting(string value) => _text.text == value ? null : StartCoroutine(WaitForSingleAnimationPlaying(value, true));

        private IEnumerator WaitForSingleAnimationPlaying(string value, bool resetText)
        {
            Cancel();
            Canceled = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                yield return _coroutine;
                _text.text = _result;
            }

            if (resetText)
                _text.text = string.Empty;

            _coroutine = StartCoroutine(WaitForAnimationPlaying(value));
            yield return _coroutine;
        }

        private IEnumerator WaitForAnimationPlaying(string value)
        {
            _result = _text.text + value;

            for (int i = 0; i < value.Length; i++)
            {
                if (_text == null)
                    break;

                _text.text += value[i];
                yield return new WaitForSeconds(_printCharDelay);
            }

            _coroutine = null;
            _text.text = _result;
        }
    }
}