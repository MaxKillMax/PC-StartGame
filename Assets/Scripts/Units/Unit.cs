using System;
using Newtonsoft.Json;
using SG.Units.Players;

namespace SG.Units
{

    [Serializable]
    public class Unit : IUnit
    {
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("id")] public int Id { get; private set; }
        [JsonProperty("strength")] public int Stength { get; private set; }
        [JsonProperty("agility")] public int Agility { get; private set; }
        [JsonProperty("lucky")] public int Lucky { get; private set; }
        [JsonProperty("endurance")] public int Endurance { get; private set; }

        [JsonIgnore] private int _health;
        [JsonIgnore] private int MaxHealth => Stat.GetMaxHealth(Endurance);
        [JsonIgnore] public int Health { get => _health; set
            {
                if (value < 0) 
                    value = 0;
                else if (value > MaxHealth)
                    value = MaxHealth;

                _health = value;
            }
        }

        /// <summary>
        /// Order: Id, Strength, Agility, Lucky, Endurance
        /// </summary>
        /// <param name="enemyInfo"></param>
        public static Unit Convert(string name, params int[] enemyInfo) => new()
        {
            Name = name,
            Id = enemyInfo[0],
            Stength = enemyInfo[1],
            Agility = enemyInfo[2],
            Lucky = enemyInfo[3],
            Endurance = enemyInfo[4],
            Health = Stat.GetMaxHealth(enemyInfo[4])
        };
    }
}
