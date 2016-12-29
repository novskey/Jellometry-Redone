using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PrefabManager : MonoBehaviour
    {
        public GameObject[] Prefabs = new GameObject[15];

        public GameObject Get(String prefabName)
        {
            foreach (GameObject prefab in Prefabs)
            {
                if (prefab.name == prefabName)
                {
                    return prefab;
                }
            }

            return new GameObject("empty");
        }

        public GameObject[] GetShrines()
        {
            List<GameObject> shrines = new List<GameObject>();
            foreach (GameObject prefab in Prefabs)
            {
                if (prefab == null) continue;

                if (prefab.tag == "BossShrine")
                {
                    shrines.Add(prefab);
                }
            }

            return shrines.ToArray();
        }
    }
}
