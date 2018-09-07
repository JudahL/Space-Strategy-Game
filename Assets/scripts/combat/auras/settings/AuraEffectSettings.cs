using UnityEngine;
using Zenject;

public abstract class AuraEffectSettings : ScriptableObject
{
    public abstract AuraEffect BuildAuraEffect(IInstantiator instantiator);
}
