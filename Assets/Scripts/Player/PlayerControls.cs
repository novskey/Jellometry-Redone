using UnityEngine;
using Assets.Scripts;
using Gamestrap;

public class PlayerControls : MonoBehaviour
{
    private Player _player;

    private GameplayUI _gameplayUi;

    // Use this for initialization
	void Start ()
	{
	     _player = GetComponentInParent<Player>();
	    _gameplayUi = GameObject.Find("UI Control").GetComponent<GameplayUI>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        float speedChanged = _player.Speed * Time.fixedDeltaTime;

        if (Input.anyKey)
        {
            if (Input.GetKey(PrefsManager.GetKeyCode("Forward")))
            {
                transform.Translate(Vector3.forward * speedChanged);
            }
            if (Input.GetKey(PrefsManager.GetKeyCode("Left")))
            {
                transform.Translate(Vector3.left * speedChanged);
            }
            if (Input.GetKey(PrefsManager.GetKeyCode("Back")))
            {
                transform.Translate(-Vector3.forward * speedChanged);
            }
            if (Input.GetKey(PrefsManager.GetKeyCode("Right")))
            {
                transform.Translate(Vector3.right * speedChanged);
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
