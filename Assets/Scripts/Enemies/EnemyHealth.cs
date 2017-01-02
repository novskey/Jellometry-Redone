using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealth : MonoBehaviour, IEnemyHealth {

        internal float _currentHealth;
        private float _maxHealth;

        public float _baseHealth = 50;

        internal IEnemy _enemy;

        private GameManager _gameManager;
        private bool _canRegen = true;

        public virtual void Start()
        {
            StatModifiers = new Dictionary<PlayerStat, float>
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

            _currentHealth = _maxHealth = BaseHealth = _baseHealth;
            _enemy = GetComponent<IEnemy>();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public void ApplyDamage(float damage)
        {
            if (_currentHealth - damage <= 0)
            {
                _currentHealth = 0;
                Die();
            }
            else
            {
                _currentHealth -= damage;
            }
        }

        public virtual void Die()
        {
            //Debug.Log("I died! I was type: " + GetComponent<IEnemy>().GetType());
            _gameManager.EnemyKilled(GetComponent<IEnemy>());
            Destroy(gameObject);
        }

        public void UpdateModifier(Mod modifier, bool add)
        {
            if (modifier.Type != "direct")
            {
                if (add)
                {
                    StatModifiers[modifier.Target] += modifier.Modifier;
                    //Debug.Log(_enemy);
                    _enemy.Buffs++;
                }
                else
                {
                    StatModifiers[modifier.Target] -= modifier.Modifier;
                    _enemy.Buffs--;
                }
            }
            else
            {
                switch (modifier.Target)
                {
                    case PlayerStat.Hp:
                        if (add)
                        {
                            _currentHealth = Math.Min(modifier.Modifier, _maxHealth);
                            _enemy.Buffs++;
                        }
                        else
                        {
                            _currentHealth -= modifier.Modifier;
                            _enemy.Buffs--;
                        }
                        break;
                    case PlayerStat.HpRegen:
                        if (add)
                        {
                            StatModifiers[modifier.Target] += modifier.Modifier;
                            _enemy.Buffs++;
                        }
                        else
                        {
                            StatModifiers[modifier.Target] -= modifier.Modifier;
                            _enemy.Buffs--;
                        }
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
            //Debug.Log(_enemy.Speed);
            _enemy.Speed = _enemy.BaseSpeed * StatModifiers[PlayerStat.Speed];
            //Debug.Log(_enemy.Speed);
//        Weapon.Damage = Weapon.BaseDamage * StatModifiers[PlayerStat.Damage];
//        Weapon.FireDelay = Weapon.BaseDelay / StatModifiers[PlayerStat.Attackspeed];
            //Debug.Log(_enemy.Damage);
            _enemy.Damage = _enemy.BaseDamage * StatModifiers[PlayerStat.Damage];
            //Debug.Log(_enemy.Damage);
//        //Debug.Log(_enemy.Damage);
//        if (Weapon.Velocity != 0)
//        {
//            Weapon.Velocity = Weapon.BaseVelocity * StatModifiers[PlayerStat.ProjectileSpeed];
//        }
            //Debug.Log(_currentHealth);
            float healthPercentage = _currentHealth / _maxHealth ;
            //Debug.Log(_maxHealth);
            //Debug.Log(healthPercentage);
            _maxHealth = BaseHealth * StatModifiers[PlayerStat.Hp];
            _currentHealth = healthPercentage * _maxHealth;
            //Debug.Log(_currentHealth);

        }

        void FixedUpdate()
        {
            try
            {
                if (StatModifiers[PlayerStat.HpRegen] > 0 && _canRegen && _currentHealth < _maxHealth)
                {
                    //Debug.Log("Regenning health");
                    StartCoroutine(RegenHp(1));
                }
            }
            catch (Exception)
            {
                Debug.Log("im fucking up: " + gameObject);
            }
        }

        private IEnumerator RegenHp(float delay)
        {
            _canRegen = false;
            yield return new WaitForSeconds(delay);
            _currentHealth = Math.Min(_currentHealth + StatModifiers[PlayerStat.HpRegen], _maxHealth);
            _gameManager.UpdateHealth();
            //Debug.Log("done!");
            _canRegen = true;
        }

        public float BaseHealth { get; set; }
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public Dictionary<PlayerStat, float> StatModifiers { get; set; }
    }
}
