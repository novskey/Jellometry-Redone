using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text ScoreText;
    public Text TimeText;
    private int _score;
    private float _startTime;
    private bool _gamestarted;

    // Use this for initialization
	void Start () {
	    _score = 0;
	}

    private void UpdateTime()
    {
        float time = Time.time - _startTime;

        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");

        TimeText.text = minutes + ":" + seconds;
    }

    // Update is called once per frame
	void Update () {
	
	}

    private void FixedUpdate()
    {
        if (_gamestarted)
        {
            UpdateTime();
        }
    }

    void UpdateScore ()
    {
        ScoreText.text = _score.ToString();
    }

    public void StartGame()
    {
        _startTime = Time.time;
        _gamestarted = true;
        UpdateScore ();
        UpdateTime();
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
                points = 20 * enemyInfo.Buffs;
                break;
        }

        Debug.Log("Player killed a: " + enemyInfo.Type + " worth " + points);

        _score += points;
        UpdateScore();
    }

}
