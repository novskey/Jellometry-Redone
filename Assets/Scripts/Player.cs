using UnityEngine;

public class Player : MonoBehaviour
{

    private float _health = 100;

    private IWeapon _weapon;

    // Use this for initialization
    void Start () {
        _weapon = new Pistol();
    }
	
    // Update is called once per frame
    void Update () {
	
    }

    void AddModifier(Mod modifier)
    {
        switch (modifier.Target)
        {
            case "hp":
                _health = (int)(_health * modifier.Modifier);
                break;
            case "fr":
                _weapon.FireDelay = _weapon.FireDelay*modifier.Modifier;
                Debug.Log(_weapon.FireDelay);
                break;
        }
    }

    void RemoveModifier(Mod modifier)
    {
        switch (modifier.Target)
        {
            case "hp":
                _health = (int)(_health / modifier.Modifier);
                break;
        }
    }


}