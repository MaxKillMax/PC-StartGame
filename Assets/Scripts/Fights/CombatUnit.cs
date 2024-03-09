using System.Collections.Generic;
using SG.Units;

namespace SG.Fights
{
    public class CombatUnit
    {
        public IUnit Unit { get; }
        public List<CombatType> Series { get; set; } = new();

        public CombatUnit(IUnit unit) 
        { 
            Unit = unit;
        }
    }
}
