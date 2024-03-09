using System.Collections.Generic;
using SG.Units;
using SG.Units.Players;
using UnityEngine;

namespace SG.Fights
{
    public class DamageCalculator
    {
        private static readonly Dictionary<CombatType, float[]> _typeAttacksPairs;

        private static readonly float[] TopAttackMultiplier = { 0, 0.5f, 0.75f, 1 };
        private static readonly float[] MiddleAttackMultiplier = { 0.8f, 0, 0.45f, 1 };
        private static readonly float[] BottomAttackMultiplier = { 0.7f, 0.65f, 0, 1 };
        private static readonly float[] NoneAttackMultiplier = { 0, 0, 0, 0 };

        static DamageCalculator()
        {
            _typeAttacksPairs = new()
            {
                { CombatType.Top, TopAttackMultiplier },
                { CombatType.Middle, MiddleAttackMultiplier },
                { CombatType.Bottom, BottomAttackMultiplier },
                { CombatType.None, NoneAttackMultiplier }
            };
        }

        public static int GetDamage(IUnit attacker, CombatType attackType, CombatType defendType) => Mathf.RoundToInt(Stat.GetDamage(attacker.Stength) * _typeAttacksPairs[attackType][(int)defendType]);
    }
}
