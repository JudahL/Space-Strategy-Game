using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverGameObject : GameplayEventObserverGeneric<GameObject>
{
    [SerializeField]
    private GameplayEventGameObject _event;
    [SerializeField]
    private UnityEventGameObject _response;

    protected override GameplayEventGeneric<GameObject> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<GameObject> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventGameObject : UnityEvent<GameObject> { }
