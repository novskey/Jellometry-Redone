using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour,IWeapon
{
    private float _velocity = 70f;
    private Rigidbody _bullet;

    // Use this for initialization
    void Start ()
    {
        Ready = true;

        Damage = 10;

        FireDelay = 0.5f;

        _bullet = Resources.Load<Rigidbody>("Prefabs/bullet");
        _bullet.GetComponent<Projectile>().SetDamage(Damage);
    }

    public void Fire()
    {
        if (Ready)
        {
            Rigidbody projClone = (Rigidbody) Instantiate(_bullet, transform.position, transform.rotation);
            projClone.GetComponent<Projectile>().SendMessage("SetDamage", Damage);
            projClone.GetComponent<Projectile>().SendMessage("SetOwner",transform.parent);
            projClone.velocity = transform.forward * _velocity;
            Destroy(projClone, 10);
            Ready = false;
            StartCoroutine(Wait(FireDelay));
        }
    }

    public float FireDelay { get; set; }
    public float Damage { get; set; }
    public bool Ready { get; set; }

    public void NotFiring()
    {
    }


    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ready = true;
    }



}