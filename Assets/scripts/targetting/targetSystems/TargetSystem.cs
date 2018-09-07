using UnityEngine;

public abstract class TargetSystem : ScriptableObject
{
    public bool _isSearching = false;
    protected ITargetSystemClient _currentClient;
    protected LayerMask _layerMask;

    public void BeginSearch(ITargetSystemClient client)
    {
        _isSearching = true;
        _currentClient = client;
        _layerMask = DEFAULT_MASK;
    }

    public void BeginSearch(ITargetSystemClient client, LayerMask mask)
    {
        BeginSearch(client);
        _layerMask = mask;
        OnBeginSearch();
    }

    public void CancelSearch(ITargetSystemClient client)
    {
        if (_currentClient == client)
        {
            _isSearching = false;
        }        
    }

    protected void AddTarget(GameObject newTarget)
    {
        bool allTargetsAcquired;

        if (_currentClient.TryAddTarget(newTarget, out allTargetsAcquired))
        {
            Debug.Log("Added: " + newTarget);
        }
        
        if (allTargetsAcquired) AllTargetsSet();
    }

    protected virtual void AllTargetsSet()
    {
        _isSearching = false;
    }

    protected virtual void OnBeginSearch() {}

    private LayerMask DEFAULT_MASK = ~0; //Default mask is set to everything;
}
