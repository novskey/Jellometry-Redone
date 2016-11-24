using UnityEngine;
using System.Collections;

public class SpeedUp : MultiplierPickUp {

    // Use this for initialization
    void Start ()
    {
        Duration = 5f;
        PowerUp = new PowerUp(3f,"speed","multiplier",Duration);
    }

}
