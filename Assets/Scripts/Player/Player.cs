using System;
using System.Collections.Generic;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        private float _baseHealth = 100;
        private float _maxHealth = 100;
        private float _currentHealth = 100;

        public float BaseSpeed = 7f;

        public float Speed = 7f;

        public IWeapon Weapon;

        public bool StartWithPistol;

        private GameManager _gameManager;

        private Dictionary<PlayerStat, float> _statModifiers = new Dictionary<PlayerStat, float>()
        {
            {PlayerStat.Hp, 1},
            {PlayerStat.Speed, 1},
            {PlayerStat.Damage, 1},
            {PlayerStat.Attackspeed, 1},
            {PlayerStat.ScoreEarned, 1}
        };

        // Use this for initialization
        void Start ()
        {
            if (StartWithPistol)
            {
                var pistolObj = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/pistol"));
                pistolObj.transform.SetParent(transform);
                pistolObj.transform.position = transform.FindChild("firePoint").position;
                pistolObj.transform.rotation = new Quaternion(0,0,0,0);

                Weapon = pistolObj.GetComponent<IWeapon>();
            }

            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        void AddModifier(Mod modifier)
        {
            if (modifier.Type != "direct")
            {
                _statModifiers[modifier.Target] += modifier.Modifier;
            }
            else
            {
                switch (modifier.Target)
                {
                    case PlayerStat.Hp:
                        _currentHealth = Math.Min(modifier.Modifier,_maxHealth);
                        break;
                    default:
                        Debug.Log("not accounted for");
                        break;
                }
            }

            UpdateStats();

//        switch
//
//        (modifier.Target)
//            {
//                case PlayerStat.Hp:
//
//                    if (modifier.Type == "direct")
//                    {
//                        _health += modifier.Modifier;
//                    }
//                    else
//                    {
//                        _health = (int)(_health * modifier.Modifier);
//                    }
//                    break;
//                case PlayerStat.Attackspeed:
//                    if (modifier.Type == "direct")
//                    {
//                        Weapon.FireDelay -= modifier.Modifier;
//                    }else{
//                        Weapon.FireDelay /= modifier.Modifier;
//                    }
//                    Debug.Log(Weapon.FireDelay);
//                    break;
//                case PlayerStat.Speed:
//                    if (modifier.Type == "direct")
//                    {
//                        Speed += modifier.Modifier;
//                    }else{
//                        Speed *= modifier.Modifier;
//                    }
//                    Debug.Log(Speed);
//                    break;
//                case PlayerStat.Damage:
//                    if (modifier.Type == "direct")
//                    {
//                        Weapon.Damage += modifier.Modifier;
//                    }else{
//                        Weapon.Damage *= modifier.Modifier;
//                    }
//                    Debug.Log(Speed);
//                    break;
//                case PlayerStat.ScoreEarned:
//                    if (modifier.Type == "direct")
//                    {
//                        ScoreMultiplier += modifier.Modifier;
//                    }else{
//                        ScoreMultiplier *= modifier.Modifier;
//                    }
//                    break;
//            }
        }

        private void UpdateStats()
        {
            Speed = BaseSpeed * _statModifiers[PlayerStat.Speed];
            Weapon.Damage = Weapon.BaseDamage * _statModifiers[PlayerStat.Damage];
            Weapon.FireDelay = Weapon.BaseDelay / _statModifiers[PlayerStat.Attackspeed];
            _maxHealth = _baseHealth * _statModifiers[PlayerStat.Hp];
        }

        void RemoveModifier(Mod modifier)
        {
            if (modifier.Type != "direct")
            {
                _statModifiers[modifier.Target] -= modifier.Modifier;
            }
            else
            {
                switch (modifier.Target)
                {
                    case PlayerStat.Hp:
                        _currentHealth -= modifier.Modifier;
                        break;
                    default:
                        Debug.Log("not accounted for");
                        break;
                }
            }

            UpdateStats();

//            switch (modifier.Target)
//            {
//                case PlayerStat.Hp:
//                    Debug.Log(_health);
//
//                    if (modifier.Type == "direct")
//                    {
//                        _health -= modifier.Modifier;
//                    }
//                    else
//                    {
//                        _health = (int)(_health / modifier.Modifier);
//                    }
//
//                    Debug.Log(_health);
//                    break;
//                case PlayerStat.Attackspeed:
//                    Debug.Log(Weapon.FireDelay);
//                    if (modifier.Type == "direct")
//                    {
//                        Weapon.FireDelay += modifier.Modifier;
//                    }else{
//                        Weapon.FireDelay *= modifier.Modifier;
//                    }
//                    Debug.Log(Weapon.FireDelay);
//                    break;
//                case PlayerStat.Speed:
//                    Debug.Log(Speed);
//                    if (modifier.Type == "direct")
//                    {
//                        Speed -= modifier.Modifier;
//                    }else{
//                        Speed /= modifier.Modifier;
//                    }
//                    Debug.Log(Speed);
//                    break;
//                case PlayerStat.Damage:
//                    if (modifier.Type == "direct")
//                    {
//                        Weapon.Damage -= modifier.Modifier;
//                    }else{
//                        Weapon.Damage /= modifier.Modifier;
//                    }
//                    Debug.Log(Speed);
//                    break;
//                case PlayerStat.ScoreEarned:
//                    if (modifier.Type == "direct")
//                    {
//                        ScoreMultiplier -= modifier.Modifier;
//                    }else{
//                        ScoreMultiplier /= modifier.Modifier;
//                    }
//                    break;
//            }
        }

        public void ApplyDamage(float damage)
        {
            if (_currentHealth - damage <= 0)
            {
                _currentHealth = 0;
            }
            else
            {
                _currentHealth -= damage;
            }

            Debug.Log("player hit");

            _gameManager.UpdateHealth();
        }

        public float Health()
        {
            return _currentHealth;
        }

        public float ScoreMultiplier()
        {
            return _statModifiers[PlayerStat.ScoreEarned];
        }
    }
}