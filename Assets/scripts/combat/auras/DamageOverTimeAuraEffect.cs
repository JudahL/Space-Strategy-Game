using Zenject;

public class DamageOverTimeAuraEffect : AuraEffect
{
    private int _amount;
    private bool _isHealing;
    private IDamageHandlerComponent _targetDamageHandler;
    private DamageOverTimeType _dotType;

    public DamageOverTimeAuraEffect(int amount, bool isHealing, DamageOverTimeType dotType, GameplayEventAuraEffectDetails auraEffectEvent)
    {
        _amount = amount;
        _isHealing = isHealing;
        _event = auraEffectEvent;
        _dotType = dotType;
        _type = GetAuraEffectType();
    }

    private AuraEffectType GetAuraEffectType()
    {
        return AuraEffectType.DamageOverTime_Fire + (int)_dotType;
    }

    public override void OnApply()
    {
        _targetDamageHandler = _target.GetComponent<IDamageHandlerComponent>();
    }

    public override void OnRemove()
    {
        _targetDamageHandler = null;
    }

    public override void OnNewTurn()
    {
        DealDamage();
    }

    private void DealDamage()
    {
        DamageType damageType = _isHealing ? DamageType.Healing : DamageType.ArmorPiercing;

        if (_targetDamageHandler != null)
        {
            DamageDetails details = new DamageDetails(_amount, damageType, _DEAL_DAMAGE_AS_CRIT, _TARGET_HAS_EVADED, null);

            _targetDamageHandler.ProcessDamage(details);
        }
    }

    // Currently not used : public class Factory : PlaceholderFactory<int, bool, AuraEffect> { }

    private static readonly bool _DEAL_DAMAGE_AS_CRIT = false;
    private static readonly bool _TARGET_HAS_EVADED = false;
}
