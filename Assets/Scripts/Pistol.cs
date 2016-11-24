using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    private int _damage = 10;
    public float _velocity = 500f;
    private bool _ready = true;
    private Rigidbody _bullet;

    // Use this for initialization
    void Start ()
    {
        FireDelay = 0.5f;

        _bullet = Resources.Load<Rigidbody>("Prefabs/bullet");
    }

    public void Fire()
    {
        if (_ready)
        {
            Rigidbody projClone = (Rigidbody) Instantiate(_bullet, transform.position, transform.rotation);
            projClone.GetComponent<Projectile>().SendMessage("SetDamage", _damage);
            projClone.velocity = transform.forward * _velocity;
            Destroy(projClone, 10);
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