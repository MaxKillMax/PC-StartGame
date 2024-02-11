using SG.Players;
using SG.UI;
using SG.Utilities;
using UnityEngine;

namespace SG
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private CharacteristicsPanel _characteristicsPanel;
        [SerializeField] private GameData _gameData;
        [SerializeField] private Game _game;

        [SerializeField] private Player _player;

        private void Awake()
        {
            new MethodFromStringExecuter(_game, _player);
        }

        private void Start()
        {
            _game.Init(_gameData.Dialogs);

            _player.Init(_gameData.PlayerInfo);
            _player.OnHealthEmptyOut += _game.Lose;
            _player.OnHealthEmptyOut += _game.DisableDialogPanel;

            _characteristicsPanel.Init(_player);
        }
    }
}