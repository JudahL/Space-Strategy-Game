using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DamageOverTime", menuName = "Auras/Effects/DamageOverTime")]
public class DamageOverTimeAuraEffectSettings : AuraEffectSettings
{
    [SerializeField]
    private bool _isHealing;
    [SerializeField]
    private int _amount;
    [SerializeField]
    private DamageOverTimeType _type;

    public override AuraEffect BuildAuraEffect(IInstantiator instantiator)
    {
        return instantiator.Instantiate<DamageOverTimeAuraEffect>(new object[] { _amount, _isHealing, _type });
    }    
}