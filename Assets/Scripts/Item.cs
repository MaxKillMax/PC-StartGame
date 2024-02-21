using System;
using System.Collections.Generic;
using SG.Players;

namespace SG.Items
{
    public static class Item
    {
        private const int FLOWER_ID = 8;

        private static readonly Dictionary<int, Func<bool>> ItemActions = new()
        {
            { FLOWER_ID, UseFlowers }
        };

        public static bool TryUse(int id)
        {
            if (!Game.Player.Inventory.HasItemInInventoryAt(id))
            {
                GameLog.Log("У вас нет этого предмета");
                return false;
            }

            if (!ItemActions.ContainsKey(id))
            {
                GameLog.Log("Вы не придумали, как использовать этот предмет");
                return false;
            }

            return ItemActions[id].Invoke();
        }

        private static bool UseFlowers()
        {
            const int enduranceAddition = 1;
            const int healPower = 10;

            Game.Player.AddStatValue(StatType.Endurance, enduranceAddition);
            Game.Player.Health.Add(healPower);
            Game.Player.Inventory.DecreaseItemInInventoryAt(FLOWER_ID);

            GameLog.Log($"Воспользовавшись растением, вы укрепили своё здоровье на {healPower} и повысили выносливость на {enduranceAddition}!");
            return true;
        }
    }
}
