namespace Assets.Scripts
{
    public interface IEnemy
    {
        EnemyType Type { get; set; }
        int Buffs { get; set; }
    }

    public enum EnemyType
    {
        Tank,
        Grunt
    }
}