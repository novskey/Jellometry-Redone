using UnityEngine;

namespace Assets.Scripts.Shrines
{
    public interface IShrine
    {

        void ApplyDamage(float damage);

        void Activate();

        float Health { get; set; }
        bool Activated { get; set; }
        GameObject ActiveModel();
    }
}