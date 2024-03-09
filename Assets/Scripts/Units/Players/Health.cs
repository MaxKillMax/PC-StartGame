using System;
using UnityEngine;

namespace SG.Units.Players
{
    public class Health
    {
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
            MaxValue = Stat.GetMaxHealth(newValue);
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