using System;
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

        private Dictionary<AoeBuff, Mod> _buffs = new Dictionary<AoeBuff, Mod>();

        private List<Collider> _buffedEnemies = new List<Collider>();

        void Start()
        {
            foreach (AoeBuff aoeBuff in transform.parent.GetComponents<AoeBuff>())
            {
                _buffs.Add(aoeBuff,
                    new Mod(aoeBuff.Target, aoeBuff.Modifier, aoeBuff.Direct ? "direct" : "multiplier"));
            }
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Player":
//                    Debug.Log("Triggered by player");
                    break;
                default:
                    foreach (AoeBuff buff in _buffs.Keys)
                    {
                        if (other.tag == buff.Tag)
                        {
//                        Debug.Log("Triggered by " + other.gameObject + ", applying buff");
                            StartCoroutine(ScaleBuff(other, 3, true, buff));
                            _buffedEnemies.Add(other);
                        }
                    }
                    break;
            }
        }

        private IEnumerator ScaleBuff(Collider other, int steps, bool up, AoeBuff buff)
        {
//            Debug.Log("Scaling up buff on " + other.gameObject);
//            Debug.Log(steps + " steps");

            float stepAmount = _buffs[buff].Modifier / steps;

            Mod _incMod = new Mod(buff.Target, stepAmount, buff.Direct ? "direct" : "multiplier");


            WaitForSeconds wait = new WaitForSeconds(1f);

            for (int i = 0; i < steps; i++)
            {
//                Debug.Log("Applied buff number " + i);
                try
                {
                    other.GetComponent<IEnemyHealth>().UpdateModifier(_incMod, up);
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                }

                yield return wait;
            }
//            Debug.Log("done scaling buff!");
        }

        private IEnumerator ScaleDownBuffs(Collider other, int steps)
        {
            foreach (KeyValuePair<AoeBuff,Mod> kv in _buffs)
            {
                StartCoroutine(ScaleBuff(other, steps, false, kv.Key));
            }

            yield return 0f;
        }


        void OnTriggerExit(Collider other)
        {
            switch (other.tag)
            {
                case "Player":
//                    Debug.Log("Player left");
                    break;
                case "Enemy":
//                    Debug.Log("Trigger by enemy, removing buff");
                    _buffedEnemies.Remove(other);
                    StartCoroutine(ScaleDownBuffs(other, 3));
                    break;
            }
        }


//
//        public void RemoveAllBuffs()
//        {
////            Debug.Log("Removing all buffs from " + _buffedEnemies.Count + " enemies");
//            foreach (Collider buffedEnemy in _buffedEnemies)
//            {
//                buffedEnemy.GetComponent<IEnemyHealth>().UpdateModifier(_mod,false);
//            }
//        }
    }
}
