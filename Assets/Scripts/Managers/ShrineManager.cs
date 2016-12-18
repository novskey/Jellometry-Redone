using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ShrineManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClearShrines()
    {
        GameObject[] shrines;
        shrines = GameObject.FindGameObjectsWithTag("Shrine");

        foreach (GameObject shrine in shrines)
        {
            Debug.Log("checking shrine: " + shrine);
            IShrine iShrine = shrine.GetComponent<IShrine>();
            if (!iShrine.Activated)
            {
                Debug.Log("moving shrine: " + shrine);
                StartCoroutine(moveShrine(shrine));
            }
        }
    }

    IEnumerator moveShrine(GameObject shrine)
    {
        for (int i = 0; i < 300; i++)
        {
            shrine.transform.Translate(0,0.1f,0);
            yield return null;
        }

        Destroy(shrine);
    }
}
