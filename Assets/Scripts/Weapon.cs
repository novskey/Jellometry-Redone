using UnityEngine;

public interface IWeapon
{

    void Fire();

    float FireDelay
    {
        get; set;
    }
}