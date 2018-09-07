using UnityEngine;

public abstract class AuraEffect
{
    protected AuraEffectType _type;
    protected GameplayEventAuraEffectDetails _event;
    protected GameObject _target;

    public void Apply(GameObject target)
    {
        _target = target;

        OnApply();

        _event.TriggerEvent(_target.GetInstanceID(), new AuraEffectDetails(_type, true));
    }

    public void Remove()
    {
        OnRemove();

        _event.TriggerEvent(_target.GetInstanceID(), new AuraEffectDetails(_type, false));
    }

    public abstract void OnApply();
    public abstract void OnRemove();

    public virtual void OnNewTurn()
    {
    }
}

//TODO: Put in seperate file
public struct AuraEffectDetails
{
    public AuraEffectType Type;
    public bool IsApplying;

    public AuraEffectDetails(AuraEffectType type, bool isApplying)
    {
        Type = type;
        IsApplying = isApplying;
    }
}
