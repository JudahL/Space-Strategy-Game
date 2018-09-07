using UnityEngine;
using Zenject;

public class AttachmentHolderComponent : MonoBehaviour
{
    [SerializeField]
    private Attachment[] _attachments;

    private int _currentIndex = 0;

    private Ship _ship;
    private Attachment.Factory _attachmentFactory;
    private AttachmentSettings[] _types;

    [Inject]
    public void Construct(Ship ship, Attachment.Factory attachmentFactory, AttachmentSettings[] attachments)
    {
        _ship = ship;
        _attachmentFactory = attachmentFactory;
        _types = attachments;
    }

    private void Start()
    {
        BuildAttachments();
    }

    private void BuildAttachments()
    {
        _attachments = new Attachment[_types.Length];
        for (int i = 0; i < _attachments.Length; i++)
        {
            _attachments[i] = _attachmentFactory.Create(_types[i]);
        }
    }

    public void ActivateAttachment()
    {
        if (_currentIndex >= _attachments.Length) return;

        _attachments[_currentIndex].Activate();

        TriggerGlobals();
    }

    public void CancelAttachment()
    {
        if (_currentIndex >= _attachments.Length) return;

        _attachments[_currentIndex].Cancel();
    }

    public AttachmentDetails SelectAttachment(int index)
    {
        _currentIndex = index;

        return _attachments[_currentIndex].SelectAttachment();
    }

    private void TriggerGlobals()
    {
        for (int i = 0; i < _attachments.Length; i++)
        {
            _attachments[i].TriggerGlobal();
        }
    }

    public Cooldown[] GetAttachmentCooldowns()
    {
        Cooldown[] cooldowns = new Cooldown[_attachments.Length];

        for (int i = 0; i < _attachments.Length; i++)
        {
            cooldowns[i] = _attachments[i].GetCooldown();
        }

        return cooldowns;
    }

    public Sprite[] GetIcons()
    {
        Sprite[] sprites = new Sprite[_attachments.Length];

        for (int i = 0; i < _attachments.Length; i++)
        {
            sprites[i] = _attachments[i].GetIcon();
        }

        return sprites;
    }
}
