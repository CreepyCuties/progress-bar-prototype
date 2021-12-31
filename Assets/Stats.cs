public class Stats
{
    public float damage;
    public float fireDamage;
    public float movementSpeed;

    public Stats(float damage, float fireDamage)
    {
        this.damage = damage;
        this.fireDamage = fireDamage;
    }
    public void Deconstruct(
        out float damage, 
        out float fireDamage
    )
    {
        damage = this.damage;
        fireDamage = this.fireDamage;
    }
}
