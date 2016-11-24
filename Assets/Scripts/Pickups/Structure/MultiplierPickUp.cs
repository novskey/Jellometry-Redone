using UnityEngine;
using System.Collections;

public class MultiplierPickUp : PickUp.ModifierPickUp
{
    public float Duration;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            CheckDecals();

            PowerUp.Activate();

            StartCoroutine(PowerUp.Remove());


            Destroy(gameObject,Duration + 0.5f);
        }
    }
}
