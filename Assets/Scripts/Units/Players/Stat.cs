using System;
using UnityEngine;

namespace SG.Units.Players
{
    [Serializable]
    public class Stat
    {
        public const int DAMAGE_PER_STRENGTH = 10;
        public const int HEALTH_PER_ENDURANCE = 50;

        public const int BASE_MISS_CHANCE = 30;
        public const int MISS_CHANCE_REDUCING_PER_LUCKY = 2;

        public const int STEP_COUNT_PER_AGILITY = 1;

        [SerializeField] private StatType _type;
        [SerializeField] private int _value;

        public string Name => nameof(_type);
        public StatType Type => _type;

        public int Value
        {
            get => _value; set
            {
                if (_value == value)
                    return;

                int pastValue = _value;
                _value = value;
                OnValueUpdated?.Invoke(pastValue, value);
            }
        }

        /// <summary>
        /// Past value, New value
        /// </summary>
        public event Action<int, int> OnValueUpdated;

        public static int GetDamage(int strength) => strength * DAMAGE_PER_STRENGTH;

        public static int GetMaxHealth(int endurance) => endurance * HEALTH_PER_ENDURANCE;

        public static int GetMissChance(int lucky) => BASE_MISS_CHANCE - lucky * MISS_CHANCE_REDUCING_PER_LUCKY;

        public static int GetStepsCount(int agility) => agility * STEP_COUNT_PER_AGILITY;
    }
}