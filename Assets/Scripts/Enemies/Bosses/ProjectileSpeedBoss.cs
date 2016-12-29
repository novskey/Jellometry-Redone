using Assets.Scripts.Bosses;

public class ProjectileSpeedBoss : Boss
{
    new void Start()
    {
        base.Start();
        Speed = BaseSpeed = 4f;
        Damage = 10f;
    }
}
