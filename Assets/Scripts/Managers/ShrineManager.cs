using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.Scripts;

public class ShrineManager : MonoBehaviour
{
    public Dictionary<string, int> ShrineLevels = new Dictionary<string, int>
    {
        {"yellow", 1},
        {"green", 1},
        {"blue", 1},
        {"purple", 1},
        {"white", 1},
        {"orange", 1},
        {"red", 1},
        {"aqua", 1}
    };

    private GameObject[] _shrineObjects;
    private PrefabManager _prefabManager;

    public Vector3[] ShrineSpots = new Vector3[4];

	// Use this for initialization
	void Start ()
	{
	    _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
	    _shrineObjects = _prefabManager.GetShrines();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject RandomShrine()
    {
        int index = Random.Range(0, _shrineObjects.Length);

        return _shrineObjects[index];
    }

    public void ClearShrines()
    {
        GameObject[] shrines = new GameObject[3];
        shrines = GameObject.FindGameObjectsWithTag("BossShrine");

        foreach (GameObject shrine in shrines)
        {
            IShrine iShrine = shrine.GetComponent<IShrine>();
            if (!iShrine.Activated)
            {
                StartCoroutine(MoveShrine(shrine));
            }
        }
    }

    IEnumerator MoveShrine(GameObject shrine)
    {
        for (int i = 0; i < 300; i++)
        {
            shrine.transform.Translate(0,0.1f,0);
            yield return null;
        }

        Destroy(shrine);
    }

    public void SpawnShrines()
    {
        List<GameObject> spawnedShrines = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            GameObject randShrine;
            do
            {
                randShrine = RandomShrine();
            } while (spawnedShrines.Contains(randShrine));

            Instantiate(randShrine, ShrineSpots[i], transform.rotation);
        }

        Instantiate(_prefabManager.Get("start shrine"), transform.position, transform.rotation);
    }
}
