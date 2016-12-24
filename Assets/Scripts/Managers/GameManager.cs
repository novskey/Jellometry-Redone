using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text ScoreText;
    public Text TimeText;
    public Text HealthText;

    private int _score;
    private float _startTime;
    private bool _gamestarted;

    private bool _paused;
    private WaveManager _waveManager;
    private Player _player;

    // Use this for initialization
    void Start () {
        _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _waveManager.WaveSetup();

        _player = GameObject.Find("player").GetComponent<Player>();

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
        HealthText.text = _player.Health().ToString();
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

        _waveManager.EnemyKilled();

        Debug.Log("Player killed a: " + enemyInfo.Type + " worth " + points);

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

}
