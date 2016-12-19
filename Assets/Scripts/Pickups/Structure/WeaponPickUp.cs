using UnityEngine;

namespace Assets.Scripts.Pickups.Structure
{
    public class WeaponPickUp : PickUp
    {
        public GameObject Weapon;

        private void Start()
        {
            Weapon = Instantiate(Weapon);
        }

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