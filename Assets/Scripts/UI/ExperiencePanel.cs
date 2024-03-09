using System;
using SG.Units.Players;
using UnityEngine;

namespace SG.UI
{
    public class ExperiencePanel : MonoBehaviour
    {
        [SerializeField] private TextPrinter _text;
        [SerializeField] private AddStatButton[] _buttons;

        private int _abilityPoints = 0;

        private Experience _experience;
        private Action<StatType, int> _addStatAction;

        public void Init(Experience experience, Action<StatType, int> addStatAction)
        {
            _experience = experience;
            _addStatAction = addStatAction;

            for (int i = 0; i < _buttons.Length; i++)
            {
                AddStatButton button = _buttons[i];
                button.OnClicked += () => IncreaseAbility(button.Type);
            }

            _experience.OnUpdated += UpdateText;
            _experience.OnMaxReached += ShowButtons;

            UpdateText();
        }

        private void UpdateText()
        {
            _ = _text.StartTextSetting(0.ToString());
        }

        private void ShowButtons()
        {
            _abilityPoints++;
            SetButtonsState(true);
        }

        private void IncreaseAbility(StatType type)
        {
            _addStatAction?.Invoke(type, 1);
            _abilityPoints--;

            if (_abilityPoints > 0)
                return;

            SetButtonsState(false);
        }

        private void SetButtonsState(bool state)
        {
            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i].gameObject.SetActive(state);
        }
    }
}