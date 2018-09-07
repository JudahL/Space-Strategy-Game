using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverShip : GameplayEventObserverGeneric<Ship>
{
    [SerializeField]
    private GameplayEventShip _event;
    [SerializeField]
    private UnityEventShip _response;

    protected override GameplayEventGeneric<Ship> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<Ship> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventShip : UnityEvent<Ship> { }
