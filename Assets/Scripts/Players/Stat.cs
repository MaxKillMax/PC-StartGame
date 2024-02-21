using System;
using UnityEngine;

namespace SG.Players
{
    [Serializable]
    public class Stat
    {
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
    }
}