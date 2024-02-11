﻿using System;
using System.Reflection;
using SG.Parameters;
using SG.Players;

namespace SG.Utilities
{
    ///<summary>
    /// Обёртка для вызова методов с помощтю строки и списка аргументов.
    /// Служит для взова функций исходя из данных которые приходят из конфигурационного файла JSON
    /// </summary>
    public class MethodFromStringExecuter
    {
        public static MethodFromStringExecuter Instance { get; private set; }

        private readonly Game _game;
        private readonly Player _player;

        public MethodFromStringExecuter(Game game, Player player)
        {
            Instance = this;

            _game = game;
            _player = player;
        }

        /// <summary>
        /// Вызывает метод с названием <c>name</c> и параметрами <c>parameters</c>
        /// </summary>
        /// <param name="name">Имя метода</param>
        /// <param name="parameters">Список параметров</param>
        public static void InvokeMethod(string name, object[] parameters)
        {
            MethodInfo method = Instance.GetType().GetMethod(name);
            method?.Invoke(Instance, parameters);
        }

        /// <summary>
        /// Вызывает метод с названием <c>name</c> и параметрами <c>parameters</c>.
        /// Возвращает флаг.
        /// </summary>
        /// <param name="name">Имя метода</param>
        /// <param name="parameters">Список параметров</param>
        /// <returns>Выполненно ли условие</returns>
        public static bool InvokeConditionMethod(string name, object[] parameters)
        {
            MethodInfo method = Instance.GetType().GetMethod(name);
            return method != null && (bool)method.Invoke(Instance, parameters);
        }

        #region Actions

        public void ChangeItemInInventoryAt(string id, string count) => _player.Inventory.ChangeItemInInventoryAt(Convert.ToInt32(id), Convert.ToInt32(count));

        public void IncreaseItemInInventoryAt(string id) => _player.Inventory.IncreaseItemInInventoryAt(Convert.ToInt32(id));

        public void DecreaseItemInInventoryAt(string id) => _player.Inventory.DecreaseItemInInventoryAt(Convert.ToInt32(id));

        public void Restart() => _game.StartGame();

        public void Win() => _game.Win();

        public void Lose() => _game.Lose();

        public void EnableParameter(string parameterName) => Parameter.Enable(parameterName);

        public void RemoveHealth(string count) => _player.RemoveHealth(Convert.ToInt32(count));

        #endregion

        #region Conditions

        public bool StateOf(string parameterName) => Parameter.StateOf(parameterName);

        public bool NotStateOf(string parameterName) => !Parameter.StateOf(parameterName);

        public bool HasItemInInventoryAt(string id) => _player.Inventory.HasItemInInventoryAt(Convert.ToInt32(id));

        #endregion
    }
}