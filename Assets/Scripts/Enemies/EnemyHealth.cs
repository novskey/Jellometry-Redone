using Assets.Scripts;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float Health = 100;

    public void ApplyDamage(float damage)
    {
        Debug.Log(this + " got hit for " + damage);
        if (Health - damage <= 0)
        {
            GameObject.Find("GameManager").SendMessage("EnemyKilled", gameObject.GetComponent<IEnemy>());
            Destroy(gameObject);
        }
        else
        {
            Health -= damage;
        }
    }
}
