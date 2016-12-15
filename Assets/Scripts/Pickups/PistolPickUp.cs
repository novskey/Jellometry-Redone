using UnityEngine;
using System.Collections;

public class PistolPickUp : PickUp.WeaponPickUp {

    private void Start()
    {
        PrefabManager prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

        Weapon = Instantiate(prefabManager.Get("pistol"));
    }
}
