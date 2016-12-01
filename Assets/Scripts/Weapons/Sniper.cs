using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Sniper : MonoBehaviour, IWeapon {

    private LineRenderer _laser;

    private bool _firing;

    private int _penetration = 2;

    private void Start()
    {
        Ready = true;

        _laser = GetComponent<LineRenderer>();
        _laser.SetVertexCount(2);

        Damage = 50;
        FireDelay = 2f;
    }

    public void Fire()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward).OrderBy(h=>h.distance).ToArray();

        if (hits.Length != 0 && !_firing)
        {
            int penetrated = 0;
            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i].transform.gameObject.tag == "Obstacle")
                {
                    _laser.SetPosition(1, hits[i].point);
                    break;
                }
                if ((hits[i].transform.gameObject.tag == "Enemy" || hits[i].transform.gameObject.tag == "Shrine") && Ready)
                {
                    hits[i].transform.gameObject.SendMessage("ApplyDamage",Damage);
                    penetrated++;
                }

                if (penetrated >= _penetration)
                {
                    _laser.SetPosition(1, hits[i].point);
                    break;
                }

                if (i == hits.Length - 1)
                {
                    _laser.SetPosition(1, hits.Last().point);
                }
            }

            if (Ready)
            {
                _firing = true;
                Ready = false;
                StartCoroutine(LaserDelay(0.1f));
                StartCoroutine(DamageDelay(FireDelay));
            }
        }
    }

    private IEnumerator LaserDelay(float f)
    {
        yield return new WaitForSeconds(f);
        _firing = false;
    }

    private void Update()
    {
        _laser.enabled = _firing;
        _laser.SetPosition(0,transform.position);
    }

    public float FireDelay { get; set; }
    public float Damage { get; set; }
    public bool Ready { get; set; }

    public void NotFiring()
    {
        _firing = false;
    }


    private IEnumerator DamageDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ready = true;
    }
}
