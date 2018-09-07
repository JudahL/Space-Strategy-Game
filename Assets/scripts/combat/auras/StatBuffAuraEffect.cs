using Zenject;

public class StatBuffAuraEffect : AuraEffect, IStatModifier
{
    private Stat _statType;
    private int _modificationAmount;
    private IBuffableStats _stats;

    public StatBuffAuraEffect(Stat type, int modAmount, GameplayEventAuraEffectDetails auraEffectEvent)
    {
        _statType = type;
        _modificationAmount = modAmount;
        _event = auraEffectEvent;
        _type = GetAuraEffectType();
    }

    private AuraEffectType GetAuraEffectType()
    {
        return AuraEffectType.StatBuff_Damage + (int)_statType;
    }

    public override void OnApply()
    {
        _stats = _target.GetComponent<IBuffableStats>();

        _stats.AddModifier(_statType, this);
    }

    public override void OnRemove()
    {
        _stats.RemoveModifier(_statType, this);
    }

    public void ApplyModification(ref int value)
    {
        value += _modificationAmount;
    }

    // Currently not used : public class Factory : PlaceholderFactory<Stat, int, AuraEffect> { }
}