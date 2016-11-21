using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        private float _health = 100;

        private IWeapon _weapon;

        // Use this for initialization
        void Start ()
        {
            _weapon = GameObject.Find("pistol").GetComponent<Pistol>();
        }

        // Update is called once per frame
        void Update () {
            if (Input.GetButton("Fire1"))
            {
                _weapon.Fire();
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
                case "fr":
                    _weapon.FireDelay = (float) (_weapon.FireDelay*modifier.Modifier);
                    Debug.Log(_weapon.FireDelay);
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


    }
}