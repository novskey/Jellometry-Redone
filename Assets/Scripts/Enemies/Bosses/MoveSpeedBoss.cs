namespace Assets.Scripts.Enemies.Bosses
{
    public class MoveSpeedBoss : Boss
    {
        new void Start()
        {
            base.Start();
            Speed = BaseSpeed = 4f;
            Damage = 10f;
        }
    }
}
