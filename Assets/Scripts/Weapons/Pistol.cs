using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour,IWeapon
{
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
            projClone.GetComponent<Projectile>().SendMessage("SetDamage", Damage);
            projClone.velocity = transform.forward * _velocity;
            Destroy(projClone, 10);
            _ready = false;
            StartCoroutine(Wait(FireDelay));
        }
    }

    public float FireDelay { get; set; }
    public float Damage { get; set; }

    public void NotFiring()
    {
    }

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        _ready = true;
    }


}