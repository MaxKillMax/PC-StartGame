using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SG.UI
{
    public class StaffElement : MonoBehaviour
    {
        [SerializeField] private GameObject _counterPanel;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _image;

        public void Init(Sprite image, Action action = null)
        {
            if (image)
                _image.sprite = image;
        }

        public void ChangeCollect(int count)
        {
            _counterPanel.SetActive(count > 0);
            _countText.text = count.ToString();
        }
    }
}