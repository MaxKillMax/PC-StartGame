using SG.Units.Players;
using SG.UI;
using SG.Utilities;
using UnityEngine;

namespace SG
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private StatsPanel _characteristicsPanel;
        [SerializeField] private GameData _gameData;
        [SerializeField] private Game _game;

        [SerializeField] private Player _player;

        private void Awake()
        {
            new MethodFromStringExecuter(_game, _player);
        }

        private void Start()
        {
            _game.Init(_gameData.Dialogs, _player);

            _player.Init(_gameData.PlayerInfo);
            _player.Health.OnEmptyOut += _game.Lose;
            _player.Health.OnEmptyOut += _game.DisableDialogPanel;

            _characteristicsPanel.Init(_player.Stats, _player.Experience, _player.AddStatValue, _player.Health);
        }
    }
}