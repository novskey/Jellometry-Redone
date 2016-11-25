using UnityEngine;
using System.Collections;

public class SniperPickUp : PickUp.WeaponPickUp {

	// Use this for initialization
	void Start () {
	    Weapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/sniper"));
	}
}
