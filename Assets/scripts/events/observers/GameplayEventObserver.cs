using UnityEngine;

public abstract class GameplayEventObserver<T> : MonoBehaviour
{
    private void OnEnable()
    {
        GetEvent().RegisterListener(this);
    }

    private void OnDisable()
    {
        GetEvent().UnregisterListener(this);
    }

    protected abstract GameplayEvent<GameplayEventObserver<T>> GetEvent();
}

