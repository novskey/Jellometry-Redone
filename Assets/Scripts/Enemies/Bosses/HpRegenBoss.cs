using Assets.Scripts.Bosses;

public class HpRegenBoss : Boss
{
    new void Start()
    {
        base.Start();
        Speed = BaseSpeed = 4f;
        Damage = 10f;
    }
}
