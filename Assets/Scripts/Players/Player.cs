using System;
using System.Collections.Generic;
using UnityEngine;

namespace SG.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private string[] _characteristicNames;

        public List<int> Characteristics { get; private set; }
        public Inventory Inventory => _inventory;

        public int MaxHealth => 100;
        public int Health { get; private set; }

        public event Action OnHealthEmptyOut;
        public event Action OnHealthChanged;

        public void Init(PlayerInfo playerInfo)
        {
            Health = MaxHealth;

            Characteristics = playerInfo.Stats;
            _inventory.Init(playerInfo.Staff);
        }

        public void RemoveHealth(int count)
        {
            Health -= count;

            if (Health < 0)
                Health = 0;

            OnHealthChanged?.Invoke();

            if (Health == 0)
                OnHealthEmptyOut?.Invoke();
        }
    }
}