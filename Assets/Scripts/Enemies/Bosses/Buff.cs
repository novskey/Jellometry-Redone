using System;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts.Enemies.Bosses
{
    public class Buff : MonoBehaviour
    {
        public float Modifier;

        public PlayerStat Target;

        public bool Direct;
    }
}