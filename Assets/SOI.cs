using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Bosses;
using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets
{
    public class SOI : MonoBehaviour
    {

        private AoeBuff _buff;

        private Mod _mod;

        private List<Collider> _buffedEnemies = new List<Collider>();

        void Start()
        {
            _buff = transform.parent.GetComponent<AoeBuff>();

            _mod = new Mod(_buff.Target, _buff.Modifier, _buff.Direct ? "direct" : "multiplier");
        }

        void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Player":
                    Debug.Log("Triggered by player");
                    break;
                default:
                    if (other.tag == _buff.Tag)
                    {
                        Debug.Log("Triggered by " + other.gameObject + ", applying buff");
                        StartCoroutine(ScaleBuff(other, 3, true));
                        _buffedEnemies.Add(other);
                    }
                    break;
            }
        }

        private IEnumerator ScaleBuff(Collider other, int steps, bool up)
        {
            Debug.Log("Scaling up buff on " + other.gameObject);
            Debug.Log(steps + " steps");
            float stepAmount = _mod.Modifier / steps;

            Mod _incMod = new Mod(_buff.Target,stepAmount, _buff.Direct ? "direct" : "multiplier");

            for (int i = 0; i < steps; i++)
            {
                Debug.Log("Applied buff number " + i);
                other.GetComponent<IEnemyHealth>().UpdateModifier(_incMod, up);
                yield return new WaitForSeconds(1);
            }
            Debug.Log("done scaling buff!");
        }

        void OnTriggerExit(Collider other)
        {
            switch (other.tag)
            {
                case "Player":
                    Debug.Log("Player left");
                    break;
                case "Enemy":
                    Debug.Log("Trigger by enemy, removing buff");
                    _buffedEnemies.Remove(other);
                    StartCoroutine(ScaleBuff(other, 3, false));
                    break;
            }
        }

        public void RemoveAllBuffs()
        {
            Debug.Log("Removing all buffs from " + _buffedEnemies.Count + " enemies");
            foreach (Collider buffedEnemy in _buffedEnemies)
            {
                buffedEnemy.GetComponent<IEnemyHealth>().UpdateModifier(_mod,false);
            }
        }
    }
}
