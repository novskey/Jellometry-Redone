using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Debug.Log("Start game");
    }

    public void EnemyKilled(EnemyType type)
    {
        int points = 0;
        switch (type)
        {
            case EnemyType.Grunt:
                points = 20;
                break;
        }

        Debug.Log("Player killed a: " + type + " worth " + points);

    }

    public void EnemyKilled(IEnemy enemyInfo)
    {
        int points = 0;
        switch (enemyInfo.Type)
        {
            case EnemyType.Grunt:
                points = 20 * enemyInfo.buffs;
                break;
        }

        Debug.Log("Player killed a: " + enemyInfo.Type + " worth " + points);

    }
}
