using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private float _health = 100;

    public void ApplyDamage(float damage)
    {
        Debug.Log(this + " got hit for " + damage);
        if (_health - damage <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _health -= damage;
        }
    }
}
