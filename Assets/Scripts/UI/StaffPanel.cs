using System.Collections.Generic;
using SG.Units.Players;
using UnityEngine;

namespace SG.UI
{
    public class StaffPanel : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private StaffElement _elementPrefab;
        [SerializeField] private Sprite[] _itemsImages;

        private List<StaffElement> _elements;

        private void Awake()
        {
            _inventory.InventoryInited.AddListener(Init);
            _inventory.InventoryChanged.AddListener(OnChangeInventory);
        }

        public void Init(List<int> staffStates)
        {
            _elements = new List<StaffElement>();

            for (int i = 0; i < staffStates.Count; i++)
            {
                int index = i;

                StaffElement element = Instantiate(_elementPrefab, transform);
                _elements.Add(element);

                element.Init(_itemsImages[_elements.IndexOf(element)], () => _inventory.Use(index));
                element.ChangeCollect(staffStates[i]);
            }
        }

        private void OnChangeInventory(int id, int newValue)
        {
            _elements[id].ChangeCollect(newValue);
        }
    }
}