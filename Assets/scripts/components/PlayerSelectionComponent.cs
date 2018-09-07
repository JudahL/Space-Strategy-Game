using UnityEngine;
using Zenject;

public class PlayerSelectionComponent : MonoBehaviour, ITargetSystemClient
{
    private TargetSystem _targetSystem;
    private TargetSearchType _shipSearchType;
    
    private int _playerId;

    [Inject]
    public void Construct(int playerId, TargetSystem targetSystem, TargetSearchType searchType)
    {
        _playerId = playerId;
        _targetSystem = targetSystem;
        _shipSearchType = searchType;
    }

    public bool TryAddTarget(GameObject target, out bool hasFoundAllTargets)
    {
        hasFoundAllTargets = false;
        
        ISelectionComponent _currentTarget = target.GetComponent<ISelectionComponent>();

        if (_currentTarget != null)
        {
            _currentTarget.Select();
            return true;
        }

        return false;
    }

    public void FindTargets()
    {
        _targetSystem.BeginSearch(this, _shipSearchType.GetLayerMask(_playerId - 10));
    }

    public void CancelFindingTargets()
    {
        _targetSystem.CancelSearch(this);
    }
}
