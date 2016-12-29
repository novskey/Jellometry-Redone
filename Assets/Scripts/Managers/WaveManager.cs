using System.Collections.Generic;
using Assets.Scripts.Spawning;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class WaveManager : MonoBehaviour
    {
        private int _wave = 0;
        private AreaSpawner[] _spawners;

        public Text WaveText;

        public static int RemainingEnemies;

        private List<GameObject> _nextWaveBosses = new List<GameObject>();

        private ShrineManager _shrineManager;

        // Use this for initialization
        void Start () {

            GameObject[] spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");

            _shrineManager = GameObject.Find("ShrineManager").GetComponent<ShrineManager>();

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
            _nextWaveBosses.Add(boss);
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
                StartCoroutine(RandomSpawner().SpawnEnemies(enemyDict));
            }


            RemainingEnemies = toSpawn + _nextWaveBosses.Count;
            if (_nextWaveBosses.Count != 0)
            {
                SpawnBosses();
            }

        }

        public void SpawnBosses()
        {
            foreach (GameObject boss in _nextWaveBosses)
            {
                RandomSpawner().SpawnBoss(boss);
            }

            _nextWaveBosses.Clear();
        }

        public AreaSpawner RandomSpawner()
        {
            return _spawners[Random.Range(0, _spawners.Length)];
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
}
