using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DisplayIcon : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    public void UpdateDetails(Sprite sprite, Vector2 position)
    {
        _image.sprite = sprite;
        GetComponent<RectTransform>().anchoredPosition = position;
    }

    public class Pool : MonoMemoryPool<Sprite, Vector2, DisplayIcon>
    {
        protected override void Reinitialize(Sprite sprite, Vector2 position, DisplayIcon icon)
        {
            icon.UpdateDetails(sprite, position);
        }
    }
}
