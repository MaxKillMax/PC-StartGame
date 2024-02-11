using System;
using System.Linq;
using SG.Dialogs;
using SG.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SG.UI
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private Button _button;

        private DialogVariant _variant;
        private Action _onCompleted;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        public bool Init(int number, DialogVariant variant, Action onCompleted)
        {
            _text.text = $"[{number}] {variant.Text}";

            _variant = variant;
            _onCompleted = onCompleted;

            bool state = true;

            foreach (SerializedMethod condition in _variant.Conditions)
            {
                object[] objArray = condition.Parameters.Cast<object>().ToArray();

                if (!MethodFromStringExecuter.InvokeConditionMethod(condition.Name, objArray))
                    state = false;
            }

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