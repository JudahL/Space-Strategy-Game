using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverAuraEffectDetails : GameplayEventObserverGeneric<AuraEffectDetails>
{
    [SerializeField]
    private GameplayEventAuraEffectDetails _event;
    [SerializeField]
    private UnityEventAuraEffectDetails _response;

    protected override GameplayEventGeneric<AuraEffectDetails> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<AuraEffectDetails> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventAuraEffectDetails : UnityEvent<AuraEffectDetails> { }
