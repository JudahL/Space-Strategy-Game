using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StatBuff", menuName = "Auras/Effects/StatBuff")]
public class StatBuffAuraEffectType : AuraEffectSettings
{
    [SerializeField]
    private Stat _statType;
    [SerializeField]
    private int _amount;

    public override AuraEffect BuildAuraEffect(IInstantiator instantiator)
    {
        return instantiator.Instantiate<StatBuffAuraEffect>(new object[] { _statType, _amount });
    }
}