using Assets.Scripts.Enemies.Bosses;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class FollowerBuff : Buff
    {
        void Start()
        {
            GetComponent<IEnemyHealth>().UpdateModifier(new Mod(Target,Modifier, Direct ? "direct" : "multiplier"), true);
        }
    }
}