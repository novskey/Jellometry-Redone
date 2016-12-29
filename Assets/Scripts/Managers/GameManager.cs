using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Managers;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text ScoreText;
    public Text TimeText;
    public Text HealthText;

    private static int _score;
    private float _startTime;
    private bool _gamestarted;

    private bool _paused;
    public Player _player;

    private static Dictionary<EnemyType, float> _enemyScores = new Dictionary<EnemyType, float>
    {
        {EnemyType.Grunt, 20},
        {EnemyType.Boss, 100}
    };

    private WaveManager _waveManager;

    // Use this for initialization
    void Start ()
    {
        _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _waveManager.WaveSetup();

        Debug.Log(PlayerPrefs.GetString("Name"));
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

    public void UpdateHealth()
    {
        Debug.Log(_player);
        HealthText.text = _player.Health().ToString();
    }

    public void UpdateScore ()
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

        _score += (int) (points * _player.ScoreMultiplier());
        UpdateScore();
    }

    public void EnemyKilled(IEnemy enemyInfo)
    {
        int points = (int) (_enemyScores[enemyInfo.GetType()] * (enemyInfo.Buffs + 1));

        Debug.Log(enemyInfo.Buffs);
        _waveManager.EnemyKilled();

        points = (int) (points* _player.ScoreMultiplier());
        Debug.Log("Player killed a: " + enemyInfo.GetType() + " worth " + points);

        _score += points;
        UpdateScore();
    }

    public void TogglePause()
    {
        if (_paused)
        {
            _paused = false;
            Time.timeScale = 1;
        }
        else
        {
            _paused = true;
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        Destroy(_player.gameObject);
        StartCoroutine(DBInterface.Connect());
        StartCoroutine(DBInterface.SaveScore(PrefsManager.GetName(),_score));
        StartCoroutine(DBInterface.CloseConnection());
    }

}
