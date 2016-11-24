using System.Collections;
using Assets.Scripts;
using UnityEngine;

public class PowerUp
{
    private float _modifier;

    private float _duration;

    private string _target;

    private string _type;

    private GameObject _playerObj;

    private Player _player;

    private Mod _mod;

    public PowerUp(float modifier, string target, string type, float duration)
    {
        _modifier = modifier;
        _duration = duration;
        _target = target;
        _type = type;
    }

    public void Activate()
    {
        _playerObj = GameObject.Find("Player");
        _player = _playerObj.GetComponent<Player>();
        _mod = new Mod(_target, _modifier, _type);
        _player.SendMessage("AddModifier",_mod);

    }

    public IEnumerator Remove() {
        Debug.Log("Start remove");
        yield return new WaitForSeconds(_duration);
        Debug.Log("Removing");
        _player.SendMessage("RemoveModifier", _mod);
    }

}