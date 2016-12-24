using UnityEngine;
using Assets.Scripts;

public class BossHealth : EnemyHealth {

    public override void Die()
    {
        GetComponent<BossReward>().Activate();
        GameObject.Find("GameManager").SendMessage("EnemyKilled", gameObject.GetComponent<IEnemy>());
        transform.position = new Vector3(0,-10000,0);
    }

    public void Reset()
    {
        Health = 100;
    }

    public ShrineManager.BossColour Colour;
}
