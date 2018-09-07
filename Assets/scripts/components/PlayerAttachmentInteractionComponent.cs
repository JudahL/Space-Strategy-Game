using UnityEngine;
using Zenject;

public class PlayerAttachmentInteractionComponent : MonoBehaviour
{
    private AttachmentHolderComponent _currentShipAttachmentHolder;

    private GameplayEventAttachmentDetails _attachmentSelectedEvent;
    private GameplayEventStandard _attachmentUnselectedEvent;

    private int _playerId;

    [Inject]
    public void Construct(int playerId, GameplayEventAttachmentDetails selectedEvent, GameplayEventStandard unselectedEvent)
    {
        _playerId = playerId;
        _attachmentSelectedEvent = selectedEvent;
        _attachmentUnselectedEvent = unselectedEvent;
    }

    public void ActivateAttachment()
    {
        if (_currentShipAttachmentHolder != null)
        {
            _currentShipAttachmentHolder.ActivateAttachment();
            _attachmentUnselectedEvent.TriggerEvent(_playerId);
        }       
    }
    
    public void CancelAttachment()
    {
        if (_currentShipAttachmentHolder != null)
        {
            _currentShipAttachmentHolder.CancelAttachment();
            _attachmentUnselectedEvent.TriggerEvent(_playerId);
        }
    }

    public void SelectAttachment(int index)
    {
        if (_currentShipAttachmentHolder != null)
        {
            AttachmentDetails attachmentDetails = _currentShipAttachmentHolder.SelectAttachment(index);

            _attachmentSelectedEvent.TriggerEvent(_playerId, attachmentDetails);
        }
    }

    public void OnSelection(GameObject target)
    {
        _currentShipAttachmentHolder = target.GetComponent<AttachmentHolderComponent>();    
    }
}