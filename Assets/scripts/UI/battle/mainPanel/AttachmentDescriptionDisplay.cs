using UnityEngine;
using UnityEngine.UI;

public class AttachmentDescriptionDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _descText;

    public void DisplayAttachmentDescription(AttachmentDetails details) 
    {
        _nameText.text = details.Name;
        _descText.text = details.Description;
    }
}
