using UnityEngine;
using System.Collections;
using System.Linq;

public class Laser : MonoBehaviour, IWeapon
{

    private LineRenderer _laser;

    private bool _firing;

    private int _penetration = 2;

    private void Start()
    {
        Ready = true;
        _laser = GetComponent<LineRenderer>();
        _laser.SetVertexCount(2);

        Damage = BaseDamage = 5;

        FireDelay = BaseDelay = 0.3f;
    }

    public void Fire()
    {
        _firing = true;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward).OrderBy(h=>h.distance).ToArray();

        if (hits.Length > 0)
        {

            _laser.SetPosition(0, transform.position);

            RaycastHit last = new RaycastHit();

            int penetrated = 0;

            bool readyThisLoop = Ready;

            bool firedThisLoop = false;

            foreach (RaycastHit raycastHit in hits)
            {
                if (raycastHit.transform.gameObject.tag == "Enemy" ||
                    raycastHit.transform.gameObject.tag == "Shrine" && Ready)
                {
                    if (readyThisLoop)
                    {
                        raycastHit.transform.gameObject.SendMessage("ApplyDamage", Damage);
                        firedThisLoop = true;
                    }
                    penetrated++;
                }
                else if (raycastHit.transform.gameObject.tag == "Obstacle")
                {
                    last = raycastHit;
                    break;
                }

                if (penetrated >= _penetration)
                {
                    last = raycastHit;
                    _laser.SetPosition(1, raycastHit.point);
                    break;
                }
            }

            if (firedThisLoop)
            {
                Ready = false;
                StartCoroutine(Wait(FireDelay));
            }

            if (last.Equals(new RaycastHit()))
            {
                _laser.SetPosition(1, hits.Last().point);
            }
            else
            {
                _laser.SetPosition(1,last.point);
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

    public float BaseDamage { get; set; }
    public float BaseDelay { get; set; }
}
