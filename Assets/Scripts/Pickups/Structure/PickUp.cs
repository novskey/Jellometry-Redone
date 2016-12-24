using UnityEngine;

public class PickUp : MonoBehaviour {
//    public PowerUp PowerUp;


    public void CheckDecals()
    {
        foreach (GameObject decal in GameObject.Find("DecalManager").GetComponent<DecalManager>().GetDecals())
        {
            float dist = (transform.position - decal.transform.position).sqrMagnitude;

            if (dist < 5)
            {
                decal.GetComponent<Decal>().BuildDecal(decal.GetComponent<Decal>());
            }
        }
    }

}


