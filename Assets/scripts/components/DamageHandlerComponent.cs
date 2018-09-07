using UnityEngine;
using Zenject;

public class DamageHandlerComponent : MonoBehaviour, IDamageHandlerComponent
{
    private IHealthComponent _healthComponent;
    private IDefensiveStats _stats;
    private SpriteDisplayComponent _spriteDisplay;
    private AuraHolderComponent _auraComponent;
    private GameplayEventCombatActionDetails _onCombatActionEvent;
    private Color _damageTintColour;
    private Color _evadedTintColour;

    public Transform EntityTransform { get { return _tr; } }

    [Inject]
    public void Construct(IHealthComponent healthComponent, IDefensiveStats stats, Transform transform,
                          AuraHolderComponent auraComponent, GameplayEventCombatActionDetails onCombatActionEvent, 
                          SpriteDisplayComponent spriteDisplay, System.Collections.Generic.List<Color> tintColours) //TODO : Replace list with struct/so and set up scriptable object to hold values
    {
        _healthComponent = healthComponent;
        _stats = stats;
        _tr = transform;
        _onCombatActionEvent = onCombatActionEvent;
        _spriteDisplay = spriteDisplay;
        _auraComponent = auraComponent;
        _damageTintColour = tintColours[0];
        _evadedTintColour = tintColours[1];
    }

    public void ProcessDamage(DamageDetails details)
    {
        ApplyDamage(details);
        ApplyAura(details);
    }

    private void ApplyDamage(DamageDetails details)
    {
        int actualValueDealt = 0;

        if (details.Damage == 0)
        {
            return;
        }

        if (details.Type != DamageType.Healing)
        {
            if (!details.TargetHasEvaded)
            {
                actualValueDealt = _healthComponent.TakeDamage(details.Damage, details.Type);
                if (actualValueDealt > 0)
                {
                    _spriteDisplay.TintSprite(_damageTintColour);
                }
            } 
            else
            {
                _spriteDisplay.TintSprite(_evadedTintColour);
            }
        }
        else
        {
            actualValueDealt = _healthComponent.Heal(details.Damage);
        }

        _onCombatActionEvent.TriggerEvent(EntityTransform.GetInstanceID(), new CombatActionDetails(actualValueDealt, GetCombatActionType(details), EntityTransform));
    }

    public bool HasEvaded()
    {
        return Random.Range(0, 100) < _stats.Evade;
    }

    private void ApplyAura(DamageDetails details)
    {
        if (details.AuraToApply != null && !details.TargetHasEvaded)
        {
            _auraComponent.AddAura(details.AuraToApply);
        }            
    }

    private CombatActionType GetCombatActionType(DamageDetails details)
    {
        CombatActionType type;
        if (details.Type == DamageType.Healing)
        {
            type = CombatActionType.Healing;
        }
        else if (details.TargetHasEvaded)
        {
            type = CombatActionType.Evade;
        } 
        else
        {
            type = CombatActionType.Damage;
        }

        if (details.IsCrit && !details.TargetHasEvaded)
        {
            type++;
        }

        return type;
    }

    private Transform _tr;
}
