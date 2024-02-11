using System.Collections.Generic;
using SG.Players;
using UnityEngine;

namespace SG.UI
{
    public class StaffPanel : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private StaffElement _elementPrefab;
        [SerializeField] private Sprite[] _itemsImages;

        private List<StaffElement> _elements;

        private void Awake()
        {
            _player.Inventory.InventoryInited.AddListener(Init);
            _player.Inventory.InventoryChanged.AddListener(OnChangeInventory);
        }

        public void Init(List<int> staffStates)
        {
            _elements = new List<StaffElement>();

            for (int i = 0; i < staffStates.Count; i++)
            {
                var newElement = Instantiate(_elementPrefab, transform);
                _elements.Add(newElement);
                newElement.Init(_itemsImages[_elements.IndexOf(newElement)]);
                newElement.ChangeCollect(staffStates[i]);
            }
        }

        private void OnChangeInventory(int id, int newValue)
        {
            _elements[id].ChangeCollect(newValue);
        }
    }
}