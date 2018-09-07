using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayEvent<T> : ScriptableObject
{
    protected List<T> listeners = new List<T>();

    public void RegisterListener(T listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(T listener)
    {
        listeners.Remove(listener);
    }
}




