using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class StartShrine : MonoBehaviour, IShrine
{
    public float _health = 30;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ApplyDamage(float damage)
    {
        if (_health - damage <= 0)
        {
            Activate();
        }
        else
        {
            _health -= damage;
        }
    }

    public void Activate()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartGame();
        Destroy(gameObject);
    }
}
