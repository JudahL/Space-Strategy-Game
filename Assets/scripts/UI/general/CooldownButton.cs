using UnityEngine.UI;
using UnityEngine;

public class CooldownButton : MonoBehaviour
{ 
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _fill;    
    [SerializeField]
    private Text _text;

    public void SetButtonInfo(Cooldown cd)
    {
        SetFill(cd.Ratio);
        SetInteractable(cd.Remaining);
        SetText(cd.Remaining);
    }

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

    private void SetFill(float value)
    {
        _fill.fillAmount = value;
    }

    private void SetInteractable(int value)
    {
        _button.interactable = value <= 0;
    }

    private void SetText(int value)
    {
        _text.enabled = value > 0;

        if (_text.enabled)
        {
            _text.text = value.ToString();
        } 
    }
}
