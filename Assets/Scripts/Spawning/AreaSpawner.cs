using UnityEngine;
using System.Collections;

public class AreaSpawner : MonoBehaviour
{
    private Bounds[] _spawnAreas;

    int _spawnCount = 0;

    // Use this for initialization
	void Start ()
	{
	    _spawnAreas = new Bounds[transform.childCount];


	    for (int i = 0; i < transform.childCount; i++)
	    {
	        if (transform.GetChild(i).tag == "Spawn Zone")
	        {
	            _spawnCount++;
	            _spawnAreas[i] = transform.GetChild(i).GetComponent<Collider>().bounds;
	        }
	    }

        StartCoroutine(spawnEnemies());
	    }

    private IEnumerator spawnEnemies()
    {
        for (int i = 0; i < 400; i++)
        {
            Instantiate(GameObject.Find("enemyTest"),RandomPoint(),transform.rotation);
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

        return new Vector3(x,0.1f,z);
    }
}
