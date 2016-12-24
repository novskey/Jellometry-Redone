using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private int _wave = 0;
    private ShrineManager _shrineManager;
    private AreaSpawner[] _spawners;

    public Text WaveText;

    public int RemainingEnemies;

	// Use this for initialization
	void Start () {
	    _shrineManager = GameObject.Find("ShrineManager").GetComponent<ShrineManager>();

	    GameObject[] spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");
	    _spawners = new AreaSpawner[spawnZones.Length];

	    for (int i = 0; i < spawnZones.Length; i++)
	    {
	        _spawners[i] = spawnZones[i].GetComponent<AreaSpawner>();
	    }
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
        Debug.Log("start wave");
        if (_wave == 0)
        {
            GameObject.Find("GameManager").SendMessage("StartGame");
        }

        _wave++;

        WaveText.text = _wave.ToString();

        _shrineManager.ClearShrines();

        SpawnWave();
    }

    private void SpawnWave()
    {
        Debug.Log("spawning wave: " + _wave);
        int spawned = 0;

        int toSpawn = _wave * 5;

        int perWave = toSpawn / _spawners.Length;

        Dictionary<string,int> enemyDict = new Dictionary<string, int>
        {
            {"enemyTest", perWave}
        };

        foreach (AreaSpawner areaSpawner in _spawners)
        {
            StartCoroutine(areaSpawner.SpawnEnemies(enemyDict));
            spawned += perWave;
            Debug.Log("spawned: " + spawned);
        }

        if (toSpawn - spawned > 0)
        {
            enemyDict["enemyTest"] = toSpawn - spawned;
            StartCoroutine(_spawners.Last().SpawnEnemies(enemyDict));
        }

        RemainingEnemies = toSpawn;
    }

    public void WaveSetup()
    {
        _shrineManager.SpawnShrines();
    }

    public void EnemyKilled()
    {
        RemainingEnemies--;
        if (RemainingEnemies == 0)
        {
            WaveSetup();
        }
    }
}
