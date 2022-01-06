public class Stats
{
    public float damage;
    public float defense;

    public Stats(float damage, float defense)
    {
        this.damage = damage;
        this.defense = damage;
    }
    public void Deconstruct(
        out float damage,
        out float defense
    )
    {
        damage = this.damage;
        defense = this.defense;
    }
}
