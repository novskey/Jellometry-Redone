using Assets.Scripts.Enemies;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Shrines
{
    public class BossShrine : MonoBehaviour, IShrine
    {

        public GameObject DefaultModel;
        public GameObject ActivatedModel;
        public GameObject Boss;
        public ShrineManager.BossColour Colour;

//    public BossReward Reward;

        // Use this for initialization
        void Start ()
        {
            Health = 30;

            DefaultModel.SetActive(true);
            ActivatedModel.SetActive(false);

            Boss.GetComponent<BossHealth>().Colour = Colour;
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void ApplyDamage(float damage)
        {
            if (Health - damage <= 0)
            {
                Activate();
            }
            else
            {
                Health -= damage;
            }
        }

        public void Activate()
        {
//        Debug.Log("big bnoi");
            DefaultModel.SetActive(false);
            ActivatedModel.SetActive(true);

            Activated = true;

            GameObject.Find("WaveManager").GetComponent<WaveManager>().SummonBoss(Boss);
        }

        public float Health { get; set; }
        public bool Activated { get; set; }

        public GameObject ActiveModel()
        {
            return Activated ? DefaultModel : ActivatedModel;
        }

        public void SaveReward(BossReward reward)
        {
        }
    }
}
