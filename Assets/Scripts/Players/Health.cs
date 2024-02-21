using System;
using UnityEngine;

namespace SG.Players
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _valuePerStat;

        public int MaxValue { get; private set; }
        public int Value { get; private set; }

        public event Action OnEmptyOut;
        public event Action OnMaxChanged;
        public event Action<int, int> OnChanged;

        public void Init(Stat stat)
        {
            stat.OnValueUpdated += UpdateMaxHealth;
            UpdateMaxHealth(default, stat.Value);
            Value = MaxValue;
        }

        private void UpdateMaxHealth(int pastValue, int newValue)
        {
            MaxValue = newValue * _valuePerStat;
            OnMaxChanged?.Invoke();
        }

        public void Add(int count)
        {
            int pastValue = Value;
            Value += count;

            if (Value > MaxValue)
                Value = MaxValue;

            if (Value == pastValue)
                return;

            OnChanged?.Invoke(pastValue, Value);
        }

        public void Remove(int count)
        {
            int pastValue = Value;
            Value -= count;

            if (Value < 0)
                Value = 0;

            if (Value == pastValue)
                return;

            OnChanged?.Invoke(pastValue, Value);

            if (Value == 0)
                OnEmptyOut?.Invoke();
        }
    }
}