using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour, IWeapon
{

    private LineRenderer _laser;

    private bool _firing;

    private void Start()
    {
        Ready = true;
        _laser = GetComponent<LineRenderer>();
        _laser.SetVertexCount(2);

        Damage = 5;
        FireDelay = 0.3f;
    }

    public void Fire()
    {
        _firing = true;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            _laser.SetPosition(0, transform.position);
            _laser.SetPosition(1, hit.point);

            if (Ready)
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    hit.transform.gameObject.SendMessage("ApplyDamage",Damage);
                }
                Ready = false;
                StartCoroutine(Wait(FireDelay));
            }
        }
        else
        {
            _firing = false;
        }
    }

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ready = true;
    }

    private void Update()
    {
        _laser.enabled = _firing;
    }

    public float FireDelay { get; set; }
    public float Damage { get; set; }
    public bool Ready { get; set; }

    public void NotFiring()
    {
        _firing = false;
    }
}
