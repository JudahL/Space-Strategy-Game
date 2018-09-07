using UnityEngine;

public class AttachmentButtons : MonoBehaviour
{
    [SerializeField]
    private CooldownButton[] _buttons;

    private AttachmentHolderComponent _attachmentHolderComponent;

    public void OnSelection(GameObject target)
    {
        _attachmentHolderComponent = target.GetComponent<AttachmentHolderComponent>();

        if (_attachmentHolderComponent != null)
        {
            UpdateCooldowns();
            UpdateSprites();
        }
    }

    public void UpdateCooldowns()
    {
        Cooldown[] cooldowns = _attachmentHolderComponent.GetAttachmentCooldowns();
        for (int i = 0; i < cooldowns.Length && i < _buttons.Length; i++)
        {
            _buttons[i].SetButtonInfo(cooldowns[i]);
        }
    }

    public void UpdateSprites()
    {
        Sprite[] sprites = _attachmentHolderComponent.GetIcons();
        for (int i = 0; i < sprites.Length && i < _buttons.Length; i++)
        {
            _buttons[i].SetIcon(sprites[i]);
        }
    }
}
