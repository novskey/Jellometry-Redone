using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Pickups.Structure;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {

        private float _baseHealth = 100, _maxHealth = 100;
        private int  _currentHealth = 100;

        public float BaseSpeed, Speed = 7f;

        public IWeapon Weapon;

        public bool StartWithPistol;

        private GameManager _gameManager;

        public float[] Modifiers;

        public static Dictionary<PlayerStat, float> StatModifiers = new Dictionary<PlayerStat, float>()
        {
            {PlayerStat.Hp, 1},
            {PlayerStat.Speed, 1},
            {PlayerStat.Damage, 1},
            {PlayerStat.Attackspeed, 1},
            {PlayerStat.ScoreEarned, 1},
            {PlayerStat.HpRegen, 0},
            {PlayerStat.ProjectileSpeed, 1},
            {PlayerStat.SlowOnHit, 1}
        };

        private bool _canRegen = true;

        // Use this for initialization
        void Start ()
        {
            //Debug.Log(_maxHealth);
            if (StartWithPistol)
            {
                var pistolObj = Instantiate(UnityEngine.Resources.Load<GameObject>("Prefabs/Weapons/pistol"));
                pistolObj.transform.SetParent(transform);
                pistolObj.transform.position = transform.FindChild("firePoint").position;
                pistolObj.transform.rotation = new Quaternion(0,0,0,0);

                Weapon = pistolObj.GetComponent<IWeapon>();
            }

            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            _gameManager.UpdateHealth();
        }

        public void UpdateModifier(Mod modifier, bool add)
        {
            if (modifier.Type != "direct")
            {
                if (add) StatModifiers[modifier.Target] += modifier.Modifier;
                else StatModifiers[modifier.Target] -= modifier.Modifier;
            }
            else
            {
                switch (modifier.Target)
                {
                    case PlayerStat.Hp:
                        if(add)  _currentHealth = (int) Math.Min(modifier.Modifier, _maxHealth);
                        else _currentHealth = (int) (_currentHealth - modifier.Modifier);
                        break;
                    case PlayerStat.HpRegen:
                        if(add) StatModifiers[modifier.Target] += modifier.Modifier;
                        else StatModifiers[modifier.Target] -= modifier.Modifier;
                        break;
                    default:
                        //Debug.Log("not accounted for");
                        break;
                }
            }

            UpdateStats();
        }

        private void UpdateStats()
        {
            Speed = BaseSpeed * StatModifiers[PlayerStat.Speed];
            Weapon.Damage = Weapon.BaseDamage * StatModifiers[PlayerStat.Damage];
            Weapon.FireDelay = Weapon.BaseDelay / StatModifiers[PlayerStat.Attackspeed];
            if (Weapon.Velocity != 0)
            {
                Weapon.Velocity = Weapon.BaseVelocity * StatModifiers[PlayerStat.ProjectileSpeed];
            }
            _maxHealth = _baseHealth * StatModifiers[PlayerStat.Hp];

            Modifiers = StatModifiers.Values.ToArray();
        }

        public void ApplyDamage(float damage)
        {
            if (_currentHealth - damage <= 0)
            {
                _currentHealth = 0;
                _gameManager.GameOver();
            }
            else
            {
                _currentHealth = (int) (_currentHealth - damage);
            }

            _gameManager.UpdateHealth();
        }

        public float Health()
        {
            return _currentHealth;
        }

        public float ScoreMultiplier()
        {
            return StatModifiers[PlayerStat.ScoreEarned];
        }

        void FixedUpdate()
        {
            if (StatModifiers[PlayerStat.HpRegen] > 0 && _canRegen && _currentHealth < _maxHealth)
            {
                //Debug.Log("Regenning health");
                StartCoroutine(RegenHp(1));
            }
        }

        private IEnumerator RegenHp(float delay)
        {
            _canRegen = false;
            yield return new WaitForSeconds(delay);
            _currentHealth = (int) Math.Min(_currentHealth + StatModifiers[PlayerStat.HpRegen], _maxHealth);
            _gameManager.UpdateHealth();
            //Debug.Log("done!");
            _canRegen = true;
        }

    }
}