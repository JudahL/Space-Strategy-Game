using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverStatBuffDetails : GameplayEventObserverGeneric<StatBuffDetails>
{
    [SerializeField]
    private GameplayEventStatBuffDetails _event;
    [SerializeField]
    private UnityEventStatBuffDetails _response;

    protected override GameplayEventGeneric<StatBuffDetails> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<StatBuffDetails> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventStatBuffDetails : UnityEvent<StatBuffDetails> { }
