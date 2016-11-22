using System.Collections;
using UnityEditor;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    private int _damage = 10;
    public float _velocity = 500f;
    private bool _ready = true;
    public Rigidbody Bullet;

    // Use this for initialization
    void Start ()
    {
        FireDelay = 0.5f;
    }

    public void Fire()
    {
        if (_ready)
        {
            Rigidbody projClone = (Rigidbody) Instantiate(Bullet, transform.position, transform.rotation);
            projClone.GetComponent<Projectile>().SendMessage("SetDamage",_damage);
            projClone.velocity = transform.forward*_velocity;
            Destroy(projClone,10);
            _ready = false;
            StartCoroutine(Wait(FireDelay));
        }
    }

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        _ready = true;
    }

    public float FireDelay { get; set; }

}