﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Shrines;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ShrineManager : MonoBehaviour
    {

        public static Dictionary<BossColour, int> ShrineLevels = new Dictionary<BossColour, int>
        {
            {BossColour.Yellow, 0},
            {BossColour.Green, 0},
            {BossColour.Blue, 0},
            {BossColour.Purple, 0},
            {BossColour.White,0},
            {BossColour.Orange, 0},
            {BossColour.Red, 0},
            {BossColour.DarkGreen, 0},
            {BossColour.Aqua, 0}
        };

        private static GameObject[] _shrineObjects;

        public Vector3[] ShrineSpots = new Vector3[4];

        private PrefabManager _prefabManager;

        // Use this for initialization
        void Start ()
        {
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
            _shrineObjects = _prefabManager.GetShrines();
        }

        public static GameObject RandomShrine()
        {
            int index = Random.Range(0, _shrineObjects.Length);

            return _shrineObjects[index];
        }

        public void PlaceStartShrine()
        {
            Instantiate(_prefabManager.Get("start shrine"), Vector3.zero, Quaternion.identity);
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
                yield return 0f;
            }

            Destroy(shrine);
        }

        public int ShrineLevel(BossColour colour)
        {
            return  ShrineLevels[colour];
        }

        public void SpawnShrines()
        {
            List<BossColour> spawnedShrines = new List<BossColour>();

            for (int i = 0; i < 4; i++)
            {
                GameObject randShrine;
                do
                {
                    randShrine = RandomShrine();
                } while (spawnedShrines.Contains(randShrine.GetComponent<BossShrine>().Colour));

                Instantiate(randShrine, ShrineSpots[i], Quaternion.identity);
                spawnedShrines.Add(randShrine.GetComponent<BossShrine>().Colour);
            }

            PlaceStartShrine();
        }

        public void ClearStartShrine()
        {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("Start Shrine"))
                Destroy(o);
        }

        public GameObject[] ShrineBosses()
        {
            List<GameObject> bosses = new List<GameObject>();
            foreach (GameObject shrineObject in _shrineObjects)
            {
                bosses.Add(shrineObject.GetComponent<BossShrine>().Boss);
            }

            return bosses.ToArray();
        }

        public static GameObject Boss(BossColour colour)
        {
            foreach (GameObject shrineObject in _shrineObjects)
            {
                if (shrineObject.GetComponent<BossShrine>().Colour == colour)
                {
                    return shrineObject.GetComponent<BossShrine>().Boss;
                }
            }

            return new GameObject();
        }
    }
}
