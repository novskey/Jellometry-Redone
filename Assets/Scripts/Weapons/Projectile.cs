using UnityEngine;
using Assets.Scripts;

public class Projectile : MonoBehaviour
{

    private float _damage;
    private Transform _owner;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform == _owner) return;
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ApplyDamage",_damage);
        }else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            other.gameObject.SendMessage("ApplyDamage",_damage);
        }else if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);
            CreateSplat(other);
        }else if (other.gameObject.tag == "Shrine")
        {
            other.gameObject.SendMessage("ApplyDamage",_damage);
        }else
        {
            CreateSplat(other);
        }

        Destroy(gameObject);
    }

    void SetOwner(Transform owner)
    {
        this._owner = owner;
    }

    void CreateSplat(Collision collision)
    {
        float y = Random.rotation.y * 100;
        float z = Random.rotation.z * 100;

        GameObject splat = (GameObject) Instantiate(Resources.Load<GameObject>("Prefabs/Splat"), transform.position, Quaternion.Euler(135, y, z));

        splat.transform.position = transform.position;
        splat.transform.forward = -collision.contacts[0].normal;
        splat.transform.Rotate(45,0,0);

        GameObject.Find("DecalManager").GetComponent<DecalManager>().AddDecal(splat);
    }

    public Transform GetOwner()
    {
        return _owner;
    }
}
