using UnityEngine;

public interface IWeapon
{

    void Fire();

    double FireDelay
    {
        get; set;
    }
}