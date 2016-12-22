using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class AreaSpawner : MonoBehaviour
{
    private Bounds[] _spawnAreas;

    private PrefabManager _prefabManager;

    int _spawnCount = 0;

    // Use this for initialization
	void Start ()
	{
	    _spawnAreas = new Bounds[transform.childCount];

	    _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

	    for (int i = 0; i < transform.childCount; i++)
	    {
	        if (transform.GetChild(i).tag == "Spawn Zone")
	        {
	            _spawnCount++;
	            _spawnAreas[i] = transform.GetChild(i).GetComponent<Collider>().bounds;
	        }
	    }

	    Dictionary<string, int> dict = new Dictionary<string, int> {{"enemyTest", 20}};
//	    StartCoroutine(SpawnEnemies(dict));
	    }

    public IEnumerator SpawnEnemies(Dictionary<string, int> enemies)
    {
        foreach (KeyValuePair<string,int> kvPair in enemies)
        {
            for (int i = 0; i < kvPair.Value; i++)
            {
                Instantiate(_prefabManager.Get(kvPair.Key),RandomPoint(),transform.rotation);
            }
        }
        yield break;
    }

    // Update is called once per frame
	void Update () {
	
	}

    public Vector3 RandomPoint()
    {
        Bounds bounds = _spawnAreas[Random.Range(0, _spawnCount)];
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x,1f,z);
    }
}
