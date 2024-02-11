using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SG.Players
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
            Items[id]--;
            InventoryChanged?.Invoke(id, Items[id]);
        }

        public bool HasItemInInventoryAt(int id) => Items[id] > 0;
    }
}

