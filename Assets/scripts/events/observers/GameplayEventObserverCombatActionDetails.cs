using UnityEngine;
using UnityEngine.Events;

public class GameplayEventObserverCombatActionDetails : GameplayEventObserverGeneric<CombatActionDetails>
{
    [SerializeField]
    private GameplayEventCombatActionDetails _event;
    [SerializeField]
    private UnityEventCombatActionDetails _response;

    protected override GameplayEventGeneric<CombatActionDetails> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<CombatActionDetails> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventCombatActionDetails : UnityEvent<CombatActionDetails> { }