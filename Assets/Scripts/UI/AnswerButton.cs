using System;
using System.Linq;
using System.Threading.Tasks;
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

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_button)
                _button = GetComponent<Button>();
        }
#endif

        public void CancelInit()
        {
            _text.Cancel();
        }

        public async Task<bool> InitAsync(int number, DialogVariant variant, Action onCompleted)
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
                await _text.SetTextAsync($"[{number}] {variant.Text}");

            _button.onClick.AddListener(Click);
            return state;
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