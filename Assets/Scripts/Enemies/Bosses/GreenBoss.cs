using Assets.Scripts;
using UnityEngine;

public class GreenBoss : MonoBehaviour, IEnemy
{
    public EnemyType Type { get; set; }
    public int Buffs { get; set; }

    void Start()
    {
        Type = EnemyType.Boss;
    }

}