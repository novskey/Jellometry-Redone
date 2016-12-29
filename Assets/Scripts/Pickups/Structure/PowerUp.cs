//using System;
//using System.Collections;
//using Assets.Scripts;
//using Assets.Scripts.Pickups.Structure;
//using UnityEngine;
//
//public class PowerUp : MonoBehaviour
//{
//    public float Modifier;
//
//    public float _duration;
//
//    public PlayerStat Target;
//
//    public string _type;
//
//    private GameObject _playerObj;
//
//    private Player _player;
//
//    private Mod _mod;
//
//    public PowerUp(float modifier, PlayerStat target, string type, float duration)
//    {
//        Modifier = modifier;
//        _duration = duration;
//        Target = target;
//        _type = type;
//    }
//
//    public void Activate()
//    {
//        _playerObj = GameObject.Find("Player");
//        _player = _playerObj.GetComponent<Player>();
//        _mod = new Mod(Target, Modifier, _type);
//        _player.SendMessage("AddModifier",_mod);
//
//    }
//
//    public IEnumerator Remove() {
//        Debug.Log("Start remove");
//        yield return new WaitForSeconds(_duration);
//        Debug.Log("Removing");
//        _player.SendMessage("RemoveModifier", _mod);
//    }
//
//    public String Type()
//    {
//        return _type;
//    }
//
//    public float Duration()
//    {
//        return _duration;
//    }
//}