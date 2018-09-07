using UnityEngine.Events;
using UnityEngine;

public class GameplayEventObserverAttachmentDetails : GameplayEventObserverGeneric<AttachmentDetails> {
    [SerializeField]
    private GameplayEventAttachmentDetails _event;
    [SerializeField]
    private UnityEventAttachmentDetails _response;

    protected override GameplayEventGeneric<AttachmentDetails> _gameplayEvent { get { return _event; } }
    protected override UnityEvent<AttachmentDetails> _eventResponse { get { return _response; } }
}

[System.Serializable]
public class UnityEventAttachmentDetails : UnityEvent<AttachmentDetails> { }