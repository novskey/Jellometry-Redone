using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Spawning
{
    public class AreaSpawner : MonoBehaviour
    {
        private Bounds[] _spawnAreas;

        int _spawnCount = 0;

        private PrefabManager _prefabManager;

        // Use this for initialization
        void Start ()
        {
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

            _spawnAreas = new Bounds[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Spawn Zone")
                {
                    _spawnCount++;
                    _spawnAreas[i] = transform.GetChild(i).GetComponent<Collider>().bounds;
                }
            }
        }

        public IEnumerator SpawnEnemies(Dictionary<BossColour, int> enemies)
        {
            WaitForSeconds wait = new WaitForSeconds(1.5f);

            foreach (KeyValuePair<BossColour, int> kv in enemies)
            {
                for (int i = 0; i < kv.Value; i++)
                {
                    Instantiate(_prefabManager.Get("follower_" + kv.Key),RandomPoint(),transform.rotation);

                    if (i % 3 == 0)
                    {
                        yield return wait;
                    }
                }
            }
        }

        public Vector3 RandomPoint()
        {
            Bounds bounds = _spawnAreas[Random.Range(0, _spawnCount)];
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);

            return new Vector3(x,1f,z);
        }

        public GameObject SpawnBoss(GameObject boss)
        {
            return (GameObject) Instantiate(boss, RandomPoint(), transform.rotation);
        }
    }
}
