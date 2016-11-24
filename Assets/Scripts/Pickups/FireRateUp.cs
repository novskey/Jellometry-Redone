using UnityEngine;
using System.Collections;

public class FireRateUp : MultiplierPickUp {

	// Use this for initialization
	void Start () {
	    Duration = 5f;
	    PowerUp = new PowerUp(1.5f,"firerate","multiplier",Duration);
	}

}
