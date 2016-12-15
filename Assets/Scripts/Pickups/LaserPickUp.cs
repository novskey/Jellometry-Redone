using UnityEngine;
using System.Collections;

public class LaserPickUp : PickUp.WeaponPickUp {

    private void Start()
    {
        PrefabManager prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

        Weapon = Instantiate(prefabManager.Get("laser"));
    }
}
