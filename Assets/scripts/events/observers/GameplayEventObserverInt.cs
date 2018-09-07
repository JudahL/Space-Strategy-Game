using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverInt : GameplayEventObserverGeneric<int>
{
    [SerializeField]
    private GameplayEventInt _event;
    [SerializeField]
    private UnityEventInt _response;

    protected override GameplayEventGeneric<int> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<int> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventInt : UnityEvent<int> { }