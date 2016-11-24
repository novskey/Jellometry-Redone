using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class PickUp : MonoBehaviour {
//    public PowerUp PowerUp;


    public void CheckDecals()
    {
        foreach (GameObject decal in GameObject.Find("DecalManager").GetComponent<DecalManager>().GetDecals())
        {
            float dist = (transform.position - decal.transform.position).sqrMagnitude;

            if (dist < 5)
            {
                decal.GetComponent<Decal>().BuildDecal(decal.GetComponent<Decal>());
            }
        }
    }

    public class ModifierPickUp : PickUp
    {
        public PowerUp PowerUp;
    }

    public class WeaponPickUp : PickUp
    {
        public GameObject Weapon;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                for (int i = 0; i < other.transform.childCount; i++)
                {
                    if (other.transform.GetChild(i).gameObject.tag == "Weapon")
                    {
                        Destroy(other.transform.GetChild(i).gameObject);
                    }
                }
            }

            Weapon.transform.SetParent(other.transform);
            Weapon.transform.position = other.gameObject.transform.FindChild("firePoint").position;
            Weapon.transform.rotation = new Quaternion(0,0,0,0);

            other.gameObject.GetComponent<Player>().Weapon = Weapon.GetComponent<IWeapon>();

            Destroy(gameObject);
        }
    }

}


