using UnityEngine;
using System.Collections;

public class RemoveCollions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
