using System;
using SG.Units.Players;
using UnityEngine;
using UnityEngine.UI;

namespace SG.UI
{
    [RequireComponent(typeof(Button))]
    public class AddStatButton : MonoBehaviour
    {
        [SerializeField] private StatType _type;
        private Button _button;

        public StatType Type => _type;

        public event Action OnClicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() => OnClicked?.Invoke();
    }
}