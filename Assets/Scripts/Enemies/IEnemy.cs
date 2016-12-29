namespace Assets.Scripts.Enemies
{
    public interface IEnemy
    {
        EnemyType Type { get; set; }

        EnemyType GetType();

        int Buffs { get; set; }
        float Damage { get; set; }
        float BaseDamage { get; set; }
        float Speed { get; set; }
        float BaseSpeed { get; set; }
    }

    public enum EnemyType
    {
        Tank,
        Grunt,
        Boss
    }
}