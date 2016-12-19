using System.Collections;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts
{
    public class BossReward
    {
        public float Modifier;

        public PlayerStat Target;

        public bool Direct;

        private GameObject _playerObj;

        private Player _player;

        private Mod _mod;

        public BossReward(float modifier, PlayerStat target, bool direct)
        {
            Modifier = modifier;
            Target = target;
            Direct = direct;
        }

        public void Activate()
        {
            _playerObj = GameObject.Find("Player");
            _player = _playerObj.GetComponent<Player>();
            _mod = new Mod(Target, Modifier, Direct ? "direct" : "multiplier");
            _player.SendMessage("AddModifier",_mod);

        }
    }
}