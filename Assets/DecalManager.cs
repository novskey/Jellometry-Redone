using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class DecalManager : MonoBehaviour
{
    public List<GameObject> _decals = new List<GameObject>();

    public void addDecal(GameObject decal)
    {
        if (_decals.Count > 10)
        {
            Destroy(_decals[0]);
            _decals.RemoveAt(0);
        }

        _decals.Add(decal);
    }
}
