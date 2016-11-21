using System.Collections;
using Assets.Scripts;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private double _modifier = 1.5;

    private float _duration = 5;

    private string _target = "hp";

    private GameObject _playerObj;

    private Player _player;

    private Mod _mod;

    // Use this for initialization
    private void Start()
    {
        _playerObj = GameObject.Find("Player");
        _player = _playerObj.GetComponent<Player>();
        _mod = new Mod(_target, _modifier);
        _player.SendMessage("AddModifier",_mod);

        StartCoroutine(Remove());
    }

    private IEnumerator Remove() {
        yield return new WaitForSeconds(_duration);
        _player.SendMessage("RemoveModifier", _mod);
        GameObject.Destroy(this);
    }
	
    // Update is called once per frame
    private void Update () {
		
    }
}