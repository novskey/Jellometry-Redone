using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    private float _health = 100;

    public void ApplyDamage(float damage)
    {
        if (_health - damage <= 0)
        {
            _health = 0;
        }
    }
}
