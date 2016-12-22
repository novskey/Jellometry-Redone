using Assets.Scripts.Pickups.Structure;

class Mod
{
    public PlayerStat Target;
    public float Modifier;
    public string Type;

    public Mod(PlayerStat target, float modifier, string type)
    {
        Target = target;
        Modifier = modifier;
        Type = type;
    }
}