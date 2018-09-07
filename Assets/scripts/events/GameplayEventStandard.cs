using UnityEngine;

[CreateAssetMenu(fileName = "GameplayEvent", menuName = "Events/GameplayEvent")]
public class GameplayEventStandard : GameplayEvent<IGameplayEventListener>
{
    public void TriggerEvent(int senderID)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventTriggered(senderID);
        }
    }

}
