using System.Linq;
using DG.Tweening;
using SG.Players;
using UnityEngine;
using UnityEngine.UI;

namespace SG.UI
{
    public class HealthPanel : MonoBehaviour
    {
        private Health _health;

        [SerializeField] private TextPrinter _text;
        [SerializeField] private Image _panel;

        [SerializeField] private float _colorChangeTime;

        [SerializeField] private Color _decreaseColor;
        [SerializeField] private Color _increaseColor;

        private Color _defaultColor;

        private Sequence _sequence;

        public void Init(Health health)
        {
            _health = health;
            _defaultColor = _panel.color;

            if (_health == null)
                return;

            UpdateHealth();

            _health.OnMaxChanged += UpdateHealth;
            _health.OnChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            if (_health == null)
                return;

            _health.OnMaxChanged -= UpdateHealth;
            _health.OnChanged -= UpdateHealth;
        }

        private void UpdateHealth(int pastValue, int newValue)
        {
            _sequence?.Kill(false);
            _sequence = DOTween.Sequence();

            _panel.color = _defaultColor;

            if (pastValue > newValue)
                _sequence.Append(_panel.DOColor(_decreaseColor, _colorChangeTime));
            else
                _sequence.Append(_panel.DOColor(_increaseColor, _colorChangeTime));

            _sequence.Append(_panel.DOColor(_defaultColor, _colorChangeTime));
            _sequence.AppendCallback(() => _sequence = null);

            UpdateHealth();
        }

        private void UpdateHealth() => _ = _text.SetTextAsync($"{_health.Value}/{_health.MaxValue}");
    }
}