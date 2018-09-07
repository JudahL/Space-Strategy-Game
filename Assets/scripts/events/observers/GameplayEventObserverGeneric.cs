using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class GameplayEventObserverGeneric<T> : MonoBehaviour, IGameplayEventListener<T>
{
    [Tooltip("Only observes events fired by the GameObject with specified ID. Use '0' to observe events from any Game Object. Use '1' when testing with editor fired events.")]
    [InjectOptional]
    public int ObservingID = 0;

    protected abstract GameplayEventGeneric<T> _gameplayEvent { get; }
    protected abstract UnityEvent<T> _eventResponse { get; }

    private void OnEnable()
    {
        _gameplayEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        _gameplayEvent.UnregisterListener(this);
    }

    public void OnEventTriggered(int senderID, T param)
    {
        if (ObservingID == 0 || ObservingID == senderID)
            _eventResponse.Invoke(param);
    }
}
