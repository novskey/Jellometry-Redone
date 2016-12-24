public interface IWeapon
{
    void Fire();

    float FireDelay{ get; set; }
    float Damage { get; set; }
    bool Ready { get; set; }
    float BaseDamage { get; set; }
    float BaseDelay { get; set; }

    void NotFiring();
}