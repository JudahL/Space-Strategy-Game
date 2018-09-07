using UnityEngine;

public interface IGameplayEventListener<T>
{
    void OnEventTriggered(int senderID, T eventArgument);   	
}

public interface IGameplayEventListener
{
    void OnEventTriggered(int senderID);
}
