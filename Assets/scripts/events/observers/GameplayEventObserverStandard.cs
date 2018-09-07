using UnityEngine.Events;
using UnityEngine;
using Zenject;

public class GameplayEventObserverStandard : MonoBehaviour, IGameplayEventListener
{
    [Tooltip("Only observes events fired by the GameObject with specified ID. Use '0' to observe events from any Game Object. Use '1' when testing with editor fired events.")]
    [InjectOptional]
    public int observingID = 0;

    public GameplayEventStandard gameplayEvent;
    public UnityEvent response;    

    private void OnEnable()
    {
        gameplayEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameplayEvent.UnregisterListener(this);
    }

    public void OnEventTriggered(int senderID)
    {
        if (observingID == 0 || observingID == senderID)
            response.Invoke();
    }
}
