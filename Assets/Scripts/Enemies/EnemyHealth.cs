using Assets.Scripts;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float _health = 100;

    public void ApplyDamage(float damage)
    {
        Debug.Log(this + " got hit for " + damage);
        if (_health - damage <= 0)
        {
            GameObject.Find("GameManager").SendMessage("EnemyKilled", gameObject.GetComponent<IEnemy>());
            Destroy(gameObject);
        }
        else
        {
            _health -= damage;
        }
    }
}
