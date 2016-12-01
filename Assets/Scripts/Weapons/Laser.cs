using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Laser : MonoBehaviour, IWeapon
{

    private LineRenderer _laser;

    private bool _firing;

    private int _penetration = 1;

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

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward).OrderBy(h=>h.distance).ToArray();

        if (hits.Length > 0)
        {

            _laser.SetPosition(0, transform.position);

            RaycastHit last = new RaycastHit();

            if (Ready)
            {
                int penetrated = 0;

                foreach (RaycastHit raycastHit in hits)
                {
                    if (raycastHit.transform.gameObject.tag == "Enemy" ||
                        raycastHit.transform.gameObject.tag == "Shrine")
                    {
                        raycastHit.transform.gameObject.SendMessage("ApplyDamage", Damage);
                        penetrated++;
                    }
                    else if (raycastHit.transform.gameObject.tag == "Obstacle")
                    {
                        last = raycastHit;
                        break;
                    }

                    Debug.Log("Penetrated " + penetrated + " out of " + _penetration);
                    if (penetrated >= _penetration)
                    {
                        Debug.Log("max penetration");
                        Debug.Log(raycastHit.transform.gameObject);
                        _laser.SetPosition(1, raycastHit.point);
                        break;
                    }
                }
                Ready = false;
                StartCoroutine(Wait(FireDelay));
            }
            else
            {
                if (last.Equals(new RaycastHit()))
                {
                    _laser.SetPosition(1, hits.Last().point);
                }
                else
                {
                    _laser.SetPosition(1,last.point);
                }
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
