using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIPresetSpriteSelectionComponent : MonoBehaviour
{
    private Sprite[] _sprites;
    private Image _imageRenderer;

    [Inject]
    public void Construct(Sprite[] sprites, Image imageRenderer)
    {
        _sprites = sprites;
        _imageRenderer = imageRenderer;
    }

    public void ChangeSprite(int index)
    {
        _imageRenderer.sprite = _sprites[index];
    }
}
