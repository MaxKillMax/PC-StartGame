using System;
using System.Linq;
using SG.Players;
using UnityEngine;

namespace SG.UI
{
    public class StatsPanel : MonoBehaviour
    {
        private Stat[] _stats;

        [SerializeField] private TextPrinter _strengthText;
        [SerializeField] private TextPrinter _agilityText;
        [SerializeField] private TextPrinter _luckyText;
        [SerializeField] private TextPrinter _enduranceText;

        [SerializeField] private TextPrinter _expText;
        [SerializeField] private HealthPanel _healthPanel;

        public void Init(Stat[] stats, Health health)
        {
            _stats = stats;

            _ = _expText.SetTextAsync(0.ToString());
            _healthPanel.Init(health);

            if (_stats == null)
                return;

            InitStat(stats.First(s => s.Type == StatType.Strength), _strengthText, UpdateStrength);
            InitStat(stats.First(s => s.Type == StatType.Agility), _agilityText, UpdateAgility);
            InitStat(stats.First(s => s.Type == StatType.Lucky), _luckyText, UpdateLucky);
            InitStat(stats.First(s => s.Type == StatType.Endurance), _enduranceText, UpdateEndurance);
        }

        private void InitStat(Stat stat, TextPrinter text, Action<int, int> onUpdated)
        {
            _ = text.SetTextAsync(stat.Value.ToString());
            stat.OnValueUpdated += onUpdated;
        }

        private void OnDestroy()
        {
            if (_stats == null)
                return;

            _stats.First(s => s.Type == StatType.Strength).OnValueUpdated -= UpdateStrength;
            _stats.First(s => s.Type == StatType.Agility).OnValueUpdated -= UpdateAgility;
            _stats.First(s => s.Type == StatType.Lucky).OnValueUpdated -= UpdateLucky;
            _stats.First(s => s.Type == StatType.Endurance).OnValueUpdated -= UpdateEndurance;
        }

        private void UpdateStrength(int pastValue, int newValue) => _ = _strengthText.SetTextAsync(newValue.ToString());

        private void UpdateAgility(int pastValue, int newValue) => _ = _agilityText.SetTextAsync(newValue.ToString());

        private void UpdateLucky(int pastValue, int newValue) => _ = _luckyText.SetTextAsync(newValue.ToString());

        private void UpdateEndurance(int pastValue, int newValue) => _ = _enduranceText.SetTextAsync(newValue.ToString());
    }
}