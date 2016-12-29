namespace Assets.Scripts.Enemies.Bosses
{
    public class MaxHpBoss : Boss
    {
        new void Start()
        {
            base.Start();
            Speed = BaseSpeed = 3f;
            Damage = 10f;
        }
    }
}