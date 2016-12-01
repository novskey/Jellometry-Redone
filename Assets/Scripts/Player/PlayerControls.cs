using UnityEngine;
using Assets.Scripts;

public class PlayerControls : MonoBehaviour
{
    private Player _player;

    // Use this for initialization
	void Start ()
	{
	     _player = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        float speedChanged = _player.Speed * Time.deltaTime;

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speedChanged);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speedChanged);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.forward * speedChanged);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speedChanged);
            }
            if (Input.GetKey("space") || Input.GetButton("Fire1"))
            {
                if (_player.Weapon != null)
                {
                    _player.Weapon.Fire();
                }
            }
        }

	    if (_player.Weapon != null && _player.transform.GetChild(1).GetComponent<Laser>() != null && !Input.GetButton("Fire1"))
	    {
	        _player.Weapon.NotFiring();
	    }
	}
}
