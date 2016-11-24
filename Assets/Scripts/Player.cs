using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        private float _health = 100;

        public float Speed = 5f;

        public IWeapon Weapon;

        // Use this for initialization
        void Start ()
        {
        }

        void AddModifier(Mod modifier)
        {
            switch (modifier.Target)
            {
                case "hp":
                    Debug.Log(_health);

                    if (modifier.Type == "direct")
                    {
                        _health += modifier.Modifier;
                    }
                    else
                    {
                        _health = (int)(_health * modifier.Modifier);
                    }

                    Debug.Log(_health);
                    break;
                case "firerate":
                    Debug.Log(Weapon.FireDelay);
                    if (modifier.Type == "direct")
                    {
                        Weapon.FireDelay -= modifier.Modifier;
                    }else{
                        Weapon.FireDelay /= modifier.Modifier;
                    }
                    Debug.Log(Weapon.FireDelay);
                    break;
                case "speed":
                    Debug.Log(Speed);
                    if (modifier.Type == "direct")
                    {
                        Speed += modifier.Modifier;
                    }else{
                        Speed *= modifier.Modifier;
                    }
                    Debug.Log(Speed);
                    break;
            }
        }

        void RemoveModifier(Mod modifier)
        {
            switch (modifier.Target)
            {
                case "hp":
                    Debug.Log(_health);

                    if (modifier.Type == "direct")
                    {
                        _health -= modifier.Modifier;
                    }
                    else
                    {
                        _health = (int)(_health / modifier.Modifier);
                    }

                    Debug.Log(_health);
                    break;
                case "firerate":
                    Debug.Log(Weapon.FireDelay);
                    if (modifier.Type == "direct")
                    {
                        Weapon.FireDelay += modifier.Modifier;
                    }else{
                        Weapon.FireDelay *= modifier.Modifier;
                    }
                    Debug.Log(Weapon.FireDelay);
                    break;
                case "speed":
                    Debug.Log(Speed);
                    if (modifier.Type == "direct")
                    {
                        Speed -= modifier.Modifier;
                    }else{
                        Speed /= modifier.Modifier;
                    }
                    Debug.Log(Speed);
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