﻿using System.Collections;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : MonoBehaviour,IWeapon
    {
        private Rigidbody _bullet;


        private PrefabManager _prefabManager;

        // Use this for initialization
        void Start ()
        {
            _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

            _bullet = _prefabManager.Get("bullet").GetComponent<Rigidbody>();
            _bullet.GetComponent<Projectile>().SetDamage(Damage);

            Ready = true;

            Damage = BaseDamage  = 10;

            FireDelay = BaseDelay = 0.5f;

            Velocity = BaseVelocity = 70f;
        }

        public void Fire()
        {
            if (Ready)
            {
                Rigidbody projClone = (Rigidbody) Instantiate(_bullet, transform.position, transform.rotation);
                projClone.GetComponent<Projectile>().SendMessage("SetDamage", Damage);
                projClone.GetComponent<Projectile>().SendMessage("SetOwner",transform.parent);
                projClone.velocity = transform.forward * Velocity;
                Destroy(projClone, 10);
                Ready = false;
                StartCoroutine(Wait(FireDelay));
            }
        }

        public float FireDelay { get; set; }
        public float Damage { get; set; }
        public bool Ready { get; set; }

        public void NotFiring()
        {
        }


        private IEnumerator Wait(float delay)
        {
            yield return new WaitForSeconds(delay);
            Ready = true;
        }

        public float BaseDamage { get; set; }
        public float BaseDelay { get; set; }
        public float Velocity { get; set; }
        public float BaseVelocity { get; set; }
    }
}