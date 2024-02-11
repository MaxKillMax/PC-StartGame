using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SG.UI
{
    [RequireComponent(typeof(Image))]
    public class Highlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float TWEEN_TIME = 0.5f;

        [SerializeField] private Color _color;
        private Color _defaultColor;

        private DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> _tween;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _defaultColor = _image.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Kill(false);
            _tween = _image.DOColor(_color, TWEEN_TIME);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Kill(false);
            _image.DOColor(_defaultColor, TWEEN_TIME);
        }
    }
}