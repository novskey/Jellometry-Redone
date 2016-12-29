using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Bosses;
using Assets.Scripts.Pickups.Structure;
using Assets.Scripts.Spawning;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class WaveManager : MonoBehaviour
    {
        public static int Wave = 0;
        private AreaSpawner[] _spawners;

        public Text WaveText;

        public static int RemainingEnemies = 0;

        private List<GameObject> _segmentBosses = new List<GameObject>();

        private ShrineManager _shrineManager;

        private PrefabManager _prefabManager;

        private List<BossColour> _segmentColours = new List<BossColour>();

        private Player.Player _player;

        private Mod _earlyStartMod = new Mod(PlayerStat.ScoreEarned, 0.5f, "multiplier");

        // Use this for initialization
        void Start () {

            GameObject[] spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");

            _shrineManager = GameObject.Find("ShrineManager").GetComponent<ShrineManager>();
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
            _player = GameObject.Find("player").GetComponent<Player.Player>();

            _spawners = new AreaSpawner[spawnZones.Length];

            for (int i = 0; i < spawnZones.Length; i++)
            {
                _spawners[i] = spawnZones[i].GetComponent<AreaSpawner>();
            }
        }

        public void SummonBoss(GameObject boss)
        {
            _segmentBosses.Add(boss);
            Debug.Log("added " + boss + " to next segment");
        }

        public void StartSegment()
        {
            Debug.Log("start segment");

            if (Wave == 0)
            {
                GameObject.Find("GameManager").SendMessage("StartGame");
            }


            _segmentBosses.Add(_prefabManager.Get("Normal Boss"));

            foreach (GameObject nextSegmentBoss in _segmentBosses)
            {
                Debug.Log(nextSegmentBoss);
                BossColour colour = nextSegmentBoss.GetComponent<BossHealth>().Colour;

                _segmentColours.Add(colour);
            }



            StartWave();

            _shrineManager.ClearShrines();
        }

        public void StartWave()
        {
            Debug.Log("start wave");

            Wave++;

            WaveText.text = Wave.ToString();


            WaveSetup();

            SpawnWave();

        }

        private void SpawnWave()
        {
            Debug.Log("spawning wave: " + Wave);


            foreach (AreaSpawner areaSpawner in _spawners)
            {
                StartCoroutine(areaSpawner.SpawnEnemies(_segmentColours));
            }

            RemainingEnemies += Wave * _spawners.Length * _segmentColours.Count;

            if (_segmentBosses.Count != 0 && Wave % 5 == 0)
            {
                SpawnBosses();
                RemainingEnemies += _segmentBosses.Count;
            }
            Debug.Log(RemainingEnemies);

        }

        public void SpawnBosses()
        {
            foreach (GameObject boss in _segmentBosses)
            {
                RandomSpawner().SpawnBoss(boss);
            }

            _segmentBosses.Clear();
        }

        public AreaSpawner RandomSpawner()
        {
            return _spawners[Random.Range(0, _spawners.Length)];
        }

        public void WaveSetup()
        {
            if (Wave % 5 == 0)
            {
                _shrineManager.SpawnShrines();
            }
            else
            {
                _shrineManager.ClearStartShrine();
                _shrineManager.PlaceStartShrine();
            }
        }

        public void EnemyKilled()
        {
            RemainingEnemies--;
            Debug.Log(RemainingEnemies);
            if (RemainingEnemies == 0)
            {
                StartWave();
            }
        }

        public void StartShrine()
        {
            if (Wave % 5 == 0)
            {
                StartSegment();
            }
            else
            {
                StartCoroutine(EarlyStartBuff());
                StartWave();
            }
        }

        private IEnumerator EarlyStartBuff()
        {
            _player.UpdateModifier(_earlyStartMod, true);
            yield return new WaitForSeconds(15);
            _player.UpdateModifier(_earlyStartMod, false);
        }
    }
}
