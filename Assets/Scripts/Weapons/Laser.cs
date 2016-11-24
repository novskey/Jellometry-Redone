using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour, IWeapon
{

    private LineRenderer _laser;

    private bool firing;

    private void Start()
    {
        _laser = GetComponent<LineRenderer>();
        _laser.SetVertexCount(2);

        Damage = 5;
        FireDelay = 0.5f;
    }

    private int _health = 200;

    private bool _ready = true;

    public void Fire()
    {
        firing = true;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            _laser.SetPosition(0, transform.position);
            _laser.SetPosition(1, hit.point);

            if (_ready)
            {
                Debug.Log(_health -= (int) Damage);
                _ready = false;
                StartCoroutine(Wait(FireDelay));
            }
        }
        else
        {
            firing = false;
        }
    }

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        _ready = true;
    }

    private void Update()
    {
        _laser.enabled = firing;
    }

    public float FireDelay { get; set; }
    public float Damage { get; set; }

    public void NotFiring()
    {
        firing = false;
    }
}
