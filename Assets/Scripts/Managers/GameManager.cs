using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text scoreText;
    private int _score;

	// Use this for initialization
	void Start () {
	    _score = 0;
	    UpdateScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void UpdateScore ()
    {
        scoreText.text = _score.ToString();
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

        _score += points;
        UpdateScore();
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

        _score += points;
        UpdateScore();
    }
}
