using System.Linq;
using UnityEngine;

namespace SG.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Health _health;

        [SerializeField] private Stat[] _stats;

        public Stat[] Stats => _stats;
        public Inventory Inventory => _inventory;
        public Health Health => _health;

        public void Init(PlayerInfo playerInfo)
        {
            for (int i = 0; i < _stats.Length && i < playerInfo.Stats.Count; i++)
                _stats[i].Value = playerInfo.Stats[i];

            _inventory.Init(playerInfo.Staff);
            _health.Init(GetStat(StatType.Endurance));
        }

        public Stat GetStat(StatType type) => _stats.FirstOrDefault((s) => s.Type == type);

        public int GetStatValue(StatType type) => _stats.FirstOrDefault((s) => s.Type == type).Value;

        public void AddStatValue(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value += value;

        public bool IsStatMore(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value > value;

        public bool IsStatLess(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value < value;

        public bool IsStatEqual(StatType type, int value) => _stats.FirstOrDefault((s) => s.Type == type).Value == value;
    }
}