using System.Collections.Generic;
using SG.Items;
using UnityEngine;
using UnityEngine.Events;

namespace SG.Units.Players
{
    public class Inventory : MonoBehaviour
    {
        public readonly List<int> Items = new();

        public UnityEvent<List<int>> InventoryInited;
        public UnityEvent<int, int> InventoryChanged;

        public void Init(List<int> staff)
        {
            staff.ForEach(o => Items.Add(o));
            InventoryInited?.Invoke(Items);
        }

        public void ChangeItemInInventoryAt(int id, int count)
        {
            if (count < 0)
                count = 0;

            Items[id] = count;
            InventoryChanged?.Invoke(id, count);
        }

        public void IncreaseItemInInventoryAt(int id)
        {
            Items[id]++;
            InventoryChanged?.Invoke(id, Items[id]);
        }

        public void DecreaseItemInInventoryAt(int id)
        {
            if (Items[id] == 0)
                return;

            Items[id]--;
            InventoryChanged?.Invoke(id, Items[id]);
        }

        public bool HasItemInInventoryAt(int id) => Items[id] > 0;

        public void Use(int id) => Item.TryUse(id);
    }
}

