using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Grunt : MonoBehaviour, IEnemy
{

    private GameObject _player;
	// Use this for initialization
	void Start ()
	{
	    Type = EnemyType.Grunt;
	    Buffs = 2;
	    _player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
	    transform.LookAt(_player.transform);


	    float speedChanged = 5 * Time.fixedDeltaTime;

	    transform.Translate(Vector3.forward * speedChanged);
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().ApplyDamage(10);
        }
    }

    public EnemyType Type { get; set; }
    public int Buffs { get; set; }
}
