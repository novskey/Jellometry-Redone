using UnityEngine;
using System.Collections;

public class PistolPickUp : PickUp.WeaponPickUp {

    private void Start()
    {
        Weapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/pistol"));
    }
}
