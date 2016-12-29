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

        public IEnumerator SpawnEnemies(List<BossColour> enemies)
        {
            foreach (BossColour colour in enemies)
            {
                for (int i = 0; i < WaveManager.Wave; i++)
                {
                    Instantiate(_prefabManager.Get("follower_" + colour),RandomPoint(),transform.rotation);
                }
            }

            yield return null;
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


        public void SpawnBoss(GameObject boss)
        {
            Instantiate(boss, RandomPoint(), transform.rotation);
        }
    }
}
