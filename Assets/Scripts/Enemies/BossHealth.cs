using Assets.Scripts.Managers;
using Assets.Scripts.Shrines;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class BossHealth : EnemyHealth {
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

        public BossColour Colour;
    }
}
