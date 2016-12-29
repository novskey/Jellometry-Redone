using System;
using UnityEngine;
using Assets.Scripts;

public class BossHealth : EnemyHealth {

    public override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        Debug.Log(_enemy.Buffs);
        GetComponent<BossReward>().Activate();
//        GetComponentInChildren<SOI>().RemoveAllBuffs();
        GameObject.Find("GameManager").SendMessage("EnemyKilled", gameObject.GetComponent<IEnemy>());
        transform.position = new Vector3(0,-10000,0);
    }

    public void Reset()
    {
        _currentHealth = 100;
    }

    public ShrineManager.BossColour Colour;
}
