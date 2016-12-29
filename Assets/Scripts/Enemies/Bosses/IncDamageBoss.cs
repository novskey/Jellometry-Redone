using Assets.Scripts.Bosses;

public class IncDamageBoss : Boss
{
    new void Start()
    {
        base.Start();
        Speed = BaseSpeed = 4f;
        Damage = 20f;
    }
}
