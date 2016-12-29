using System;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts.Bosses
{
    public class AOEBuff : MonoBehaviour
    {
        public float Modifier;

        public PlayerStat Target;

        public bool Direct;

        public String Tag;
    }
}