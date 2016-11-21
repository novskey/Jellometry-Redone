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
        if (other.gameObject.tag == "Projectile")
        {
            float y = Random.rotation.y * 100;
            float z = Random.rotation.z*100;

            GameObject splat = (GameObject) Instantiate(GameObject.Find("Splat"), other.transform.position,Quaternion.Euler(90,y,z));

            Destroy(other.gameObject);
        }else if (other.gameObject.name != "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
