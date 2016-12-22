using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using Assets.Scripts;

public class StartShrine : MonoBehaviour, IShrine
{


    public GameObject DefaultModel;
    public GameObject ActivatedModel;

    // Use this for initialization
	void Start ()
	{

	    Health = 30;
	    DefaultModel.SetActive(true);
	    ActivatedModel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ApplyDamage(float damage)
    {
        if (Health - damage <= 0)
        {
            Activate();
        }
        else
        {
            Health -= damage;
        }
    }

    public void Activate()
    {

        Activated = true;
        DefaultModel.SetActive(false);
        ActivatedModel.SetActive(true);

        GameObject.Find("WaveManager").GetComponent<WaveManager>().StartWave();
    }

    public float Health { get; set; }
    public bool Activated { get; set; }

    public GameObject ActiveModel()
    {
        return Activated ? DefaultModel : ActivatedModel;
    }
}
