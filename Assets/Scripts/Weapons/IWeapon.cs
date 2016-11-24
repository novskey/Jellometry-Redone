using UnityEngine;

public interface IWeapon
{
    void Fire();

    float FireDelay
    {
        get; set;
    }

    float Damage { get; set; }

    void NotFiring();
}