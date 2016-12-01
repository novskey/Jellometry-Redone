namespace Assets.Scripts
{
    public interface IEnemy
    {
        EnemyType Type { get; set; }
        int buffs { get; set; }
    }

    public enum EnemyType
    {
        Tank,
        Grunt
    }
}