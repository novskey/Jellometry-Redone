using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Bosses;

public class SOI : MonoBehaviour
{

    private AOEBuff _aoeBuff;

    private Mod _mod;

    private List<Collider> _buffedEnemies = new List<Collider>();

    void Start()
    {
        _aoeBuff = transform.parent.GetComponent<AOEBuff>();

        _mod = new Mod(_aoeBuff.Target, _aoeBuff.Modifier, _aoeBuff.Direct ? "direct" : "multiplier");
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
                if (other.tag == _aoeBuff.Tag)
                {
                    Debug.Log("Triggered by " + other.gameObject + ", applying buff");
                    other.GetComponent<IEnemyHealth>().UpdateModifier(_mod, true);
                    _buffedEnemies.Add(other);
                }
                break;
        }
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
                other.GetComponent<IEnemyHealth>().UpdateModifier(_mod, false);
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
