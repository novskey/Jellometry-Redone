using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Bosses;
using Assets.Scripts.Pickups.Structure;
using Assets.Scripts.Spawning;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Managers
{
    public class WaveManager : MonoBehaviour
    {
        public static int Wave = 0;
        public static int Segment = 1;
        public static int Group = 1;

        private AreaSpawner[] _spawners;

        public Text WaveText;

        public static int RemainingEnemies = 0;

        private List<GameObject> _segmentBosses = new List<GameObject>();

        private ShrineManager _shrineManager;

        private PrefabManager _prefabManager;

        private List<BossColour> _segmentColours = new List<BossColour>();

        private Player.Player _player;

        private Mod _earlyStartMod = new Mod(PlayerStat.ScoreEarned, 0.5f, "multiplier");

        private int segmentSize = 3;

        private int _megaBossSegment = 2;

        private HashSet<BossColour> _megaBossColours = new HashSet<BossColour>();

        private GameObject _normalBoss;
        private GameObject _megaBoss;

        // Use this for initialization
        void Start()
        {

            GameObject[] spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");

            _shrineManager = GameObject.Find("ShrineManager").GetComponent<ShrineManager>();
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
            _player = GameObject.Find("player").GetComponent<Player.Player>();

            _spawners = new AreaSpawner[spawnZones.Length];

            for (int i = 0; i < spawnZones.Length; i++)
            {
                _spawners[i] = spawnZones[i].GetComponent<AreaSpawner>();
            }

            _normalBoss = _prefabManager.Get("Normal Boss");

            _megaBoss = _prefabManager.Get("Mega Boss");
        }

        public void SummonBoss(GameObject boss)
        {
            _segmentBosses.Add(boss);
            ////Debug.Log("added " + boss + " to next segment");
        }

        public void StartSegment()
        {
            Debug.Log("start segment");

            if (Wave == 0)
            {
                GameObject.Find("GameManager").SendMessage("StartGame");
            }


            foreach (GameObject nextSegmentBoss in _segmentBosses)
            {
                //Debug.Log(nextSegmentBoss);
                BossColour colour = nextSegmentBoss.GetComponent<BossHealth>().Colour;

                _segmentColours.Add(colour);
                _megaBossColours.Add(colour);
            }


            _segmentBosses.Add(_normalBoss);
            _segmentColours.Add(BossColour.Normal);

            //Debug.Log(_segmentBosses.Count);
            WaveSetup();
            StartWave();

            _shrineManager.ClearShrines();
        }

        public void StartWave()
        {
            //Debug.Log("start wave");

            Wave++;
            Debug.Log("wave: " + Wave);
            Debug.Log("group: " + Group);
            Debug.Log("segment size: " + segmentSize);
            Debug.Log("segment: " + Segment);
            WaveText.text = Wave - (Segment - 1)*segmentSize + " / " + segmentSize;


//            WaveSetup();


            SpawnWave();

        }

        private void SpawnWave()
        {
            //Debug.Log("spawning wave: " + Wave);


            Dictionary<BossColour, int> enemyDict = new Dictionary<BossColour, int>();

//            int toSpawn = (int) Math.Log(Wave) + 1;
//            (int) Math.Pow(Math.E,Wave - 1)

//            int toSpawn = Wave;
            int toSpawn = 1;

            foreach (BossColour colour in _segmentColours)
            {
//                enemyDict.Add(colour, );
                enemyDict.Add(colour, toSpawn);
                Debug.Log(enemyDict[colour]);
            }

            foreach (AreaSpawner areaSpawner in _spawners)
            {
                StartCoroutine(areaSpawner.SpawnEnemies(enemyDict));
            }

            RemainingEnemies += toSpawn * _segmentColours.Count     * _spawners.Length;

            Debug.Log(Wave + " , " + segmentSize);

            if (Wave % segmentSize == 0)
            {
                RemainingEnemies += _segmentBosses.Count;
                SpawnBosses();

                if (Wave != 0 && Wave % (segmentSize * _megaBossSegment) == 0)
                {
                    SpawnMegaBoss();
                }
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
            _segmentColours.Clear();
        }


        private void SpawnMegaBoss()
        {
            GameObject megaBoss = RandomSpawner().SpawnBoss(_megaBoss);

            megaBoss.GetComponent<Boss>().Type = EnemyType.MegaBoss;

            foreach (BossColour colour in _megaBossColours)
            {
                try
                {
                    AoeBuff bossBuff = ShrineManager.Boss(colour).GetComponent<AoeBuff>();

                    AoeBuff newBuff = megaBoss.AddComponent<AoeBuff>();


                    newBuff.Tag = bossBuff.Tag;
                    newBuff.Direct = bossBuff.Direct;
                    newBuff.Target = bossBuff.Target;
                    newBuff.Modifier = bossBuff.Modifier;
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("no AOEBuff found on boss");
                }

                try
                {
                    FollowerBuff bossBuff = _prefabManager.Get("follower_" + colour).GetComponent<FollowerBuff>();

                    FollowerBuff newBuff = megaBoss.AddComponent<FollowerBuff>();

                    newBuff.Direct = bossBuff.Direct;
                    newBuff.Target = bossBuff.Target;
                    newBuff.Modifier = bossBuff.Modifier;
                }
                catch (Exception)
                {
                    Debug.Log("no followerbuff for boss colour");
                }


            }
        }

        public AreaSpawner RandomSpawner()
        {
            return _spawners[Random.Range(0, _spawners.Length)];
        }

        public void WaveSetup()
        {

            _shrineManager.ClearStartShrine();
            _shrineManager.SpawnShrines();

        }

        public void EnemyKilled()
        {
            RemainingEnemies--;
            //Debug.Log(RemainingEnemies);
            if (RemainingEnemies == 0)
            {
                EndWave();
            }
        }

        public void EndWave()
        {
            Debug.Log(Wave - (Segment - 1)*segmentSize);
            Debug.Log("ended wave: " + Wave);
            if ((Wave + 1) % segmentSize == 0)
            {
                Debug.Log("final wave of segment");
                _shrineManager.ClearStartShrine();
            }
            else
            {
                _shrineManager.ClearStartShrine();
                _shrineManager.PlaceStartShrine();
            }

            if (Wave % segmentSize == 0)
            {
                Debug.Log("segment ended");
                Segment++;
                _segmentColours.Clear();
                WaveSetup();
            }
            else
            {
                StartWave();
            }

            Debug.Log("new wave: " + Wave);
        }

        public void StartShrine()
        {
            Debug.Log("Start shrine hit");
            Debug.Log(Wave - (Segment - 1)*segmentSize);
            if (Wave - (Segment - 1)*segmentSize == 0)
            {
                StartSegment();
            }
            else
            {
                StartCoroutine(EarlyStartBuff());
                EndWave();
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
