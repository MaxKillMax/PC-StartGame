using SG.Players;
using TMPro;
using UnityEngine;

namespace SG.UI
{
    public class CharacteristicsPanel : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private TMP_Text _forceText;
        [SerializeField] private TMP_Text _dexterityText;
        [SerializeField] private TMP_Text _luckText;
        [SerializeField] private TMP_Text _enduranceText;
        [SerializeField] private TMP_Text _expText;
        [SerializeField] private TMP_Text _healthText;

        public void Init(Player player)
        {
            _player = player;

            _forceText.text = player.Characteristics[0].ToString();
            _dexterityText.text = player.Characteristics[1].ToString();
            _luckText.text = player.Characteristics[2].ToString();
            _enduranceText.text = player.Characteristics[3].ToString();
            _expText.text = 0.ToString();
            _healthText.text = $"{player.Health}/{player.MaxHealth}";

            player.OnHealthChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.OnHealthChanged -= UpdateHealth;
        }

        private void UpdateHealth()
        {
            _healthText.text = $"{_player.Health}/{_player.MaxHealth}";
        }
    }
}