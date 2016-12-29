using System.Collections.Generic;
using Assets.Scripts.Pickups.Structure;

namespace Assets.Scripts
{
    public interface IEnemyHealth
    {
        float BaseHealth { get; set; }
        float MaxHealth { get; set; }
        float CurrentHealth { get; set; }
        Dictionary<PlayerStat, float> StatModifiers { get; set; }

        void UpdateModifier(Mod mod, bool b);
    }
}