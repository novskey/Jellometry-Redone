using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        private float _health = 100;

        public float Speed = 0f;

        public IWeapon Weapon;

        // Use this for initialization
        void Start ()
        {
            Weapon = GameObject.Find("pistol").GetComponent<Pistol>();
        }

        // Update is called once per frame
        void Update () {
            if (Input.GetButton("Fire1"))
            {
                Weapon.Fire();
            }
        }

        void AddModifier(Mod modifier)
        {
            switch (modifier.Target)
            {
                case "hp":
                    Debug.Log(_health);
                    _health = (int)(_health * modifier.Modifier);
                    Debug.Log(_health);
                    break;
                case "firerate":
                    Weapon.FireDelay = (float) (Weapon.FireDelay*modifier.Modifier);
                    Debug.Log(Weapon.FireDelay);
                    break;
                case "speed":
                    Speed = (int) (Speed * modifier.Modifier);
                    break;
            }
        }

        void RemoveModifier(Mod modifier)
        {
            switch (modifier.Target)
            {
                case "hp":
                    _health = (int)(_health / modifier.Modifier);
                    Debug.Log(_health);
                    break;
            }
        }

        public void ApplyDamage(float damage)
        {
            if (_health - damage <= 0)
            {
                _health = 0;
            }
        }
    }
}