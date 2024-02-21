using System;
using DG.Tweening;
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

        [SerializeField] private Button _button;
        [SerializeField] private float _punchForce;
        [SerializeField] private float _punchDuration;

        private Vector3 _defaultScale;

        public void Init(Sprite image, Action action = null)
        {
            if (image)
                _image.sprite = image;

            _defaultScale = transform.localScale;

            _button.onClick.AddListener(() => action?.Invoke());
            _button.onClick.AddListener(Animate);
        }

        private void Animate()
        {
            transform.localScale = _defaultScale;
            transform.DOPunchScale(Vector3.one * _punchForce, _punchDuration);
        }

        public void ChangeCollect(int count)
        {
            _counterPanel.SetActive(count > 0);
            _countText.text = count.ToString();
        }
    }
}