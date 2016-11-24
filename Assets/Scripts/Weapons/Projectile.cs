using UnityEngine;
using Assets.Scripts;

public class Projectile : MonoBehaviour
{

    private float _damage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().ApplyDamage(_damage);
        }else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().ApplyDamage(_damage);
        }else if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);

            CreateSplat(other);
        }else
        {
            CreateSplat(other);
        }

    }

    void CreateSplat(Collision collision)
    {
        float y = Random.rotation.y * 100;
        float z = Random.rotation.z * 100;

        GameObject splat = (GameObject) Instantiate(Resources.Load<GameObject>("Prefabs/Splat"), transform.position, Quaternion.Euler(135, y, z));







        splat.transform.position = transform.position;
        splat.transform.forward = -collision.contacts[0].normal;
        splat.transform.Rotate(45,0,0);

        Destroy(gameObject);

        GameObject.Find("DecalManager").GetComponent<DecalManager>().AddDecal(splat);
    }
}
