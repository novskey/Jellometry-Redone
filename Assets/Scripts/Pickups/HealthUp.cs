using System.Security.Cryptography;
using UnityEngine;

public class HealthUp : DirectPickUp
{

	// Use this for initialization
	void Start () {
	    PowerUp = new PowerUp(50,"hp","direct",0);
	}

}
