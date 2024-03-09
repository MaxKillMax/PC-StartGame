using SG.Units;
using SG.Units.Players;
using TMPro;
using UnityEngine;

namespace SG.UI
{
    public class FightStatsPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        //[SerializeField] private TMP_Text _titleText;
        //[SerializeField] private TMP_Text _currentHealthText; 

        //[SerializeField] private TMP_Text _damageText;
        //[SerializeField] private TMP_Text _stepCountText;

        //[SerializeField] private TMP_Text _missChanceText;

        private IUnit _unit;

        public void SetUnit(IUnit unit)
        {
            _unit = unit;
            UpdateStats();
        }

        public void UpdateStats()
        {
            _text.text = $"{_unit.Name}. Здоровье: {_unit.Health}. Урон: {Stat.GetDamage(_unit.Stength)}. Шанс промаха: {Stat.GetMissChance(_unit.Lucky)}. Кол-во действий: {Stat.GetStepsCount(_unit.Agility)}";

            //_titleText.text = _unit.Name;
            //_currentHealthText.text = _unit.Health.ToString();
            //_damageText.text = Stat.GetDamage(_unit.Stength).ToString();
            //_stepCountText.text = Stat.GetStepsCount(_unit.Agility).ToString();
            //_missChanceText.text = Stat.GetMissChance(_unit.Lucky).ToString();
        }
    }
}