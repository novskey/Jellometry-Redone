using UnityEngine;
using System.Collections;

public class DirectPickUp : PickUp.ModifierPickUp {

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PowerUp.Activate();

            gameObject.GetComponent<MeshRenderer>().enabled = false;

            CheckDecals();

            Destroy(gameObject);
        }
    }
}
