using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Grunt : MonoBehaviour, IEnemy
{
	// Use this for initialization
	void Start ()
	{
	    Type = EnemyType.Grunt;
	    buffs = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public EnemyType Type { get; set; }
    public int buffs { get; set; }
}
