using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
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
                _player.Weapon.Fire();
            }
        }
    }
}
