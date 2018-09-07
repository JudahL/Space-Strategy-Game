using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour, ITargetSystemClient
{
    [SerializeField]
    private TargetSystem _targetSystem;
    [SerializeField]
    private RaycastTargetSystem _targetSystemInput;
    [SerializeField]
    private PlayerAttachmentInteractionComponent _attachmentInteractionComponent;
    [SerializeField]
    private TargetSearchType _searchTypeFriendly;
    [SerializeField]
    private TargetSearchType _searchTypeOpponent;
    [SerializeField]
    private UnityEvent _onCompleteCycle;

    private WaitForSeconds _waitDuration = new WaitForSeconds(1.2f);

    private bool isSearchForFriendlyShips = false;

    private AttachmentDetails _currentAttachmentDetails;

    private List<AttachmentHolderComponent> _friendlyTargets = new List<AttachmentHolderComponent>();
    private List<Transform> _opponentTargets = new List<Transform>();

    private int _currentSelectionIndex;

    public void BeginTurn()
    {
        ResetTargets();
        StartCoroutine(Turn());
    }

    private void ResetTargets()
    {
        _friendlyTargets.Clear();
        _opponentTargets.Clear();
    }

    private IEnumerator Turn()
    {
        int _firstFriendlySelectionIndex = Random.Range(0, _friendlyTargets.Count);

        SearchForFriendlyShips();
        yield return _waitDuration;
        for (int i = 0; i < _friendlyTargets.Count; i++)
        {
            _currentSelectionIndex = (_firstFriendlySelectionIndex + i) % _friendlyTargets.Count;
            SearchForOpponentShips();
            SelectShip();
            yield return _waitDuration;
            SelectAttachment();
            SelectTargets();
            yield return _waitDuration;
            UseAttachment();
            yield return _waitDuration;
        }
        yield return _waitDuration;
        _onCompleteCycle.Invoke();
    }

    private void SearchForFriendlyShips()
    {
        isSearchForFriendlyShips = true;
        _targetSystem.BeginSearch(this, _searchTypeFriendly.GetLayerMask(1));
    }

    private void SearchForOpponentShips()
    {
        isSearchForFriendlyShips = false;
        _targetSystem.BeginSearch(this, _searchTypeOpponent.GetLayerMask(1));
    }

    private void SelectShip()
    {        
        _targetSystemInput.OnInput(Camera.main.WorldToScreenPoint(_friendlyTargets[_currentSelectionIndex].transform.position));
    }

    private void SelectAttachment()
    {
        Cooldown[] cooldowns = _friendlyTargets[_currentSelectionIndex].GetAttachmentCooldowns();
        List<int> validIndexes = new List<int>();

        for (int i = 0; i < cooldowns.Length; i++)
        {
            if (cooldowns[i].Remaining <= 0)
            {
                validIndexes.Add(i);
            }
        }

        int _attachmentSelectionIndex = Random.Range(0, validIndexes.Count);
        _attachmentInteractionComponent.SelectAttachment(validIndexes[_attachmentSelectionIndex]);
    }

    private void SelectTargets()
    {
        int _opponentSelectionIndex = Random.Range(0, _opponentTargets.Count);
        for (int i = 0; i < _currentAttachmentDetails.MaximumTargets; i++)
        {
            _targetSystemInput.OnInput(Camera.main.WorldToScreenPoint(_opponentTargets[ (_opponentSelectionIndex + i) % _opponentTargets.Count].position));
        }        
    }

    private void UseAttachment()
    {
        _attachmentInteractionComponent.ActivateAttachment();
    }

    public bool TryAddTarget(GameObject target, out bool allTargetsAcquired)
    {
        allTargetsAcquired = false;

        if (isSearchForFriendlyShips)
        {
            return TryAddFriendly(target);
        } 
        else
        {
            return TryAddOpponent(target);
        }
    }

    private bool TryAddFriendly(GameObject target)
    {
        AttachmentHolderComponent targetAttachmentHolder = target.GetComponent<AttachmentHolderComponent>();
        if (targetAttachmentHolder != null)
        {
            _friendlyTargets.Add(targetAttachmentHolder);
            //_targetAddedEvent.TriggerEvent(GetInstanceID(), target);

            return true;
        }

        return false;
    }

    private bool TryAddOpponent(GameObject target)
    {
        Transform targetTransform = target.GetComponent<Transform>();
        if (targetTransform != null)
        {
            _opponentTargets.Add(targetTransform);
            //_targetAddedEvent.TriggerEvent(GetInstanceID(), target);

            return true;
        }

        return false;
    }

    public void AddSelectedAttachmentDetails(AttachmentDetails attachmentDetails)
    {
        _currentAttachmentDetails = attachmentDetails;
    }
}
