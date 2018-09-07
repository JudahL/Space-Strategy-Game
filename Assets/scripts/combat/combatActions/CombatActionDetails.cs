using UnityEngine;

public enum CombatActionType
{
    Damage, 
    DamageCrit,
    Evade,
    Healing,
    HealingCrit
}

public struct CombatActionDetails 
{
    public int Value;
    public CombatActionType Type;
    public Transform TargetTransform;

    public CombatActionDetails(int value, CombatActionType type, Transform targetTransform)
    {
        Value = value;
        Type = type;
        TargetTransform = targetTransform;
    }
}
