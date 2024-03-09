using System;
using System.Linq;
using SG.Units.Players;
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

        [SerializeField] private ExperiencePanel _experiencePanel;
        [SerializeField] private HealthPanel _healthPanel;

        public void Init(Stat[] stats, Experience experience, Action<StatType, int> addStatAction, Health health)
        {
            _stats = stats;

            _experiencePanel.Init(experience, addStatAction);
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
            _ = text.StartTextSetting(stat.Value.ToString());
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

        private void UpdateStrength(int pastValue, int newValue) => _ = _strengthText.StartTextSetting(newValue.ToString());

        private void UpdateAgility(int pastValue, int newValue) => _ = _agilityText.StartTextSetting(newValue.ToString());

        private void UpdateLucky(int pastValue, int newValue) => _ = _luckyText.StartTextSetting(newValue.ToString());

        private void UpdateEndurance(int pastValue, int newValue) => _ = _enduranceText.StartTextSetting(newValue.ToString());
    }
}