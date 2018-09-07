using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverProjectileDetails : GameplayEventObserverGeneric<ProjectileDetails>
{
    [SerializeField]
    private GameplayEventProjectileDetails _event;
    [SerializeField]
    private UnityEventProjectileDetails _response;

    protected override GameplayEventGeneric<ProjectileDetails> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<ProjectileDetails> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventProjectileDetails : UnityEvent<ProjectileDetails> { }
