using System;
using UnityEngine;
using System.Collections;

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

        return gameObject;
    }
}
