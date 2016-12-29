using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Pickups.Structure
{
    public class ModifierPickUp : PickUp
    {

        public float Modifier;

        public float Duration;

        public PlayerStat Target;

        public bool Direct;

        private GameObject _playerObj;

        private Player.Player _player;

        private Mod _mod;

        public IEnumerator Activate()
        {
            _playerObj = GameObject.Find("Player");
            _player = _playerObj.GetComponent<Player.Player>();
            _mod = new Mod(Target, Modifier, Direct ? "direct" : "multiplier");


            _player.UpdateModifier(_mod, true);

            if (_mod.Type == "multiplier")
            {
                yield return new WaitForSeconds(Duration);
                _player.UpdateModifier(_mod, false);
            }
            _player.UpdateModifier(_mod, false);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Direct)
                {
                    Activate();

                    gameObject.GetComponent<MeshRenderer>().enabled = false;

                    CheckDecals();
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<Collider>().enabled = false;
                    CheckDecals();

                    Activate();
                }
            }
        }
    }
}