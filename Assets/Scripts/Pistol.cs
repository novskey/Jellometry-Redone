using System.Collections;
using UnityEditor;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    private int _damage = 10;
    private float _velocity = 20f;
    private bool _ready = true;
    public Rigidbody Projectile;

    // Use this for initialization
    void Start ()
    {
        FireDelay = 0.5f;
    }

    public void Fire()
    {
        if (_ready)
        {
            Rigidbody projClone = (Rigidbody) Instantiate(Projectile, transform.position, transform.rotation);
            projClone.velocity = transform.forward*_velocity;
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