using UnityEngine;
using System.Collections;

public class LaserPickUp : PickUp.WeaponPickUp {

    private void Start()
    {
        Weapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/laser"));
    }
}
