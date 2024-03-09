using System.Linq;
using SG.Fights;
using UnityEngine;

namespace SG.Units.Players
{
    public class Player : MonoBehaviour, IUnit
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Stat[] _stats;

        public Stat[] Stats => _stats;
        public Inventory Inventory => _inventory;
        public Health Health { get; } = new();
        public Experience Experience { get; } = new();

        public string Name => "Игрок";
        public int Id => 0;

        public int Stength => _stats[0].Value;
        public int Agility => _stats[1].Value;
        public int Lucky => _stats[2].Value;
        public int Endurance => _stats[3].Value;

        int IUnit.Health 
        { 
            get => Health.Value; set 
            {
                if (value == Health.Value)
                    return;

                if (value > Health.Value)
                    Health.Add(value - Health.Value);
                else
                    Health.Remove(Health.Value - value);
            } 
        }

        public void Init(PlayerInfo playerInfo)
        {
            for (int i = 0; i < _stats.Length && i < playerInfo.Stats.Count; i++)
                _stats[i].Value = playerInfo.Stats[i];

            _inventory.Init(playerInfo.Staff);
            Health.Init(GetStat(StatType.Endurance));
        }

        public Stat GetStat(StatType type) => _stats.FirstOrDefault((s) => s.Type == type);

        public int GetStatValue(StatType type) => _stats.FirstOrDefault((s) => s.Type == type).Value;

        public void AddStatValue(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value += value;

        public bool IsStatMore(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value > value;

        public bool IsStatLess(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value < value;

        public bool IsStatEqual(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value == value;
    }
}