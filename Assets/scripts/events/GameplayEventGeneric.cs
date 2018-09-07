using UnityEngine;

public class GameplayEventGeneric<T> : GameplayEvent<IGameplayEventListener<T>>
{
    public void TriggerEvent(int senderID, T arg)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventTriggered(senderID, arg);
        }
    }
}
