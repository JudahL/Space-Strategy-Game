public interface IHealthComponent
{
    int TakeDamage(int amount, DamageType type = DamageType.Standard);
    int Heal(int amount);
    int GetHealth();
}
