using UnityEngine;

public class Pistol : IWeapon
{
    private int _damage = 10;
    private int _velocity;
    public Rigidbody Projectile;

    // Use this for initialization
    void Start ()
    {
        FireDelay = 0.5;
    }

    public void Fire()
    {
        throw new System.NotImplementedException();
    }

    public double FireDelay { get; set; }

}