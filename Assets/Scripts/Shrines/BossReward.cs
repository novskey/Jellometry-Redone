using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts
{
    public class BossReward : MonoBehaviour
    {
        public float[] Modifiers = new float[5];

        public PlayerStat Target;

        public bool Direct;

        private GameObject _playerObj;

        private Player _player;

        private Mod _mod;

        private ShrineManager _shrineManager;

        void Start()
        {
            _playerObj = GameObject.FindWithTag("Player");
            _player = _playerObj.GetComponent<Player>();

            _shrineManager = GameObject.Find("ShrineManager").GetComponent<ShrineManager>();
        }


        public void Activate()
        {
            Debug.Log("boss colour: " + GetComponent<BossHealth>().Colour);
            int level = _shrineManager.ShrineLevel(GetComponent<BossHealth>().Colour);


            Debug.Log(transform + " activating at level: " + level);

            if (level > 0)
            {
                _mod = new Mod(Target, Modifiers[level] - Modifiers[level - 1], Direct ? "direct" : "multiplier");
            }
            else
            {
                _mod = new Mod(Target, Modifiers[level], Direct ? "direct" : "multiplier");
            }
            _player.UpdateModifier(_mod,true);

            GameObject.Find("ShrineManager").GetComponent<ShrineManager>().ShrineLevels[GetComponent<BossHealth>().Colour]++;
        }
    }
}