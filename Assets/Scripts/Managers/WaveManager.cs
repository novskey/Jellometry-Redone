using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    private int _wave = 0;
    private ShrineManager _shrineManager;

	// Use this for initialization
	void Start () {
	    _shrineManager = GameObject.Find("ShrineManager").GetComponent<ShrineManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SummonBoss(GameObject boss)
    {
        Debug.Log("added " + boss + " to next wave");
    }

    public void StartWave()
    {
        if (_wave == 0)
        {
            GameObject.Find("GameManager").SendMessage("StartGame");
        }

        _wave++;

        _shrineManager.ClearShrines();

    }
}
