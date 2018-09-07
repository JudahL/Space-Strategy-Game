public interface IBuffableStats
{
    void AddModifier(Stat stat, IStatModifier modifier);
    void RemoveModifier(Stat stat, IStatModifier modifier);
}