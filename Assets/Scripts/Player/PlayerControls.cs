using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerControls : MonoBehaviour
    {
        private Player _player;

        // Use this for initialization
        void Start ()
        {
            _player = GetComponentInParent<Player>();
        }
	
        // Update is called once per frame
        void FixedUpdate()
        {
            float speedChanged = _player.Speed * Time.fixedDeltaTime;

            if (Input.anyKey)
            {
                if (Input.GetKey(PrefsManager.GetKeyCode("Forward")))
                {
                    transform.Translate(PrefsManager.Forward() * speedChanged);
                }
                if (Input.GetKey(PrefsManager.GetKeyCode("Left")))
                {
                    transform.Translate(PrefsManager.Left() * speedChanged);
                }
                if (Input.GetKey(PrefsManager.GetKeyCode("Back")))
                {
                    transform.Translate(-PrefsManager.Forward() * speedChanged);
                }
                if (Input.GetKey(PrefsManager.GetKeyCode("Right")))
                {
                    transform.Translate(PrefsManager.Right() * speedChanged);
                }
                if (Input.GetKey(PrefsManager.GetKeyCode("Shoot")))
                {
                    if (_player.Weapon != null)
                    {
                        _player.Weapon.Fire();
                    }
                }
            }

            if (_player.Weapon != null && _player.transform.GetChild(1).GetComponent<Laser>() != null &&
                !Input.GetKey(PrefsManager.GetKeyCode("Shoot")))
            {
                _player.Weapon.NotFiring();
            }
        }
    }
}
