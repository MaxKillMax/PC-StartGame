using System;
using System.Collections;
using System.Linq;
using NaughtyAttributes;
using SG.Dialogs;
using SG.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace SG.UI
{
    [RequireComponent(typeof(Button))]
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private TextPrinter _text;
        [SerializeField, ReadOnly] private Button _button;

        private DialogVariant _variant;
        private Action _onCompleted;

        public bool IsInited { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_button)
                _button = GetComponent<Button>();
        }
#endif

        public Coroutine Init(int number, DialogVariant variant, Action onCompleted)
        {
            StopAllCoroutines();
            return StartCoroutine(WaitForInit(number, variant, onCompleted));
        }

        private IEnumerator WaitForInit(int number, DialogVariant variant, Action onCompleted)
        {
            _variant = variant;
            _onCompleted = onCompleted;

            bool state = true;

            foreach (SerializedMethod condition in _variant.Conditions)
            {
                object[] objArray = condition.Parameters.Cast<object>().ToArray();

                if (!MethodFromStringExecuter.InvokeConditionMethod(condition.Name, objArray))
                    state = false;
            }

            if (state)
                yield return _text.StartTextSetting($"[{number}] {variant.Text}");

            _button.onClick.AddListener(Click);
            IsInited = state;
        }

        public void Click()
        {
            foreach (SerializedMethod action in _variant.Actions)
            {
                object[] objArray = action.Parameters.Cast<object>().ToArray();
                MethodFromStringExecuter.InvokeMethod(action.Name, objArray);
            }

            _onCompleted?.Invoke();
        }
    }
}