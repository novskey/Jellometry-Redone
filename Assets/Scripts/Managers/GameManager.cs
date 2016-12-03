using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text scoreText;
    public Text TimeText;
    private int _score;
    private float startTime;

    // Use this for initialization
	void Start () {
	    _score = 0;

	    startTime = Time.time;
	    UpdateScore ();
	    UpdateTime();
	}

    private void UpdateTime()
    {
        float time = Time.time - startTime;

        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");

        TimeText.text = minutes + ":" + seconds;
    }

    // Update is called once per frame
	void Update () {
	
	}

    private void FixedUpdate()
    {
        UpdateTime();
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
