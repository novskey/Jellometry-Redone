using UnityEngine;

namespace Assets.Scripts
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