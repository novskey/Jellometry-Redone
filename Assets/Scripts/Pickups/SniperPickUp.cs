using UnityEngine;
using System.Collections;

public class SniperPickUp : PickUp.WeaponPickUp {

	// Use this for initialization
	void Start () {
	    PrefabManager prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

	    Weapon = Instantiate(prefabManager.Get("sniper"));
	}
}
