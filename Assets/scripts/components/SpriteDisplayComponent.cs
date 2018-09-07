using Zenject;
using UnityEngine;
using System.Collections;

public class SpriteDisplayComponent : MonoBehaviour
{
    private Sprite _sprite;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColour;

    private bool spriteHasBeenSet = false;

    [Inject]
    public void Construct(Sprite sprite, SpriteRenderer spriteRenderer)
    {
        _sprite = sprite;
        _spriteRenderer = spriteRenderer;
    }

    private void Start()
    {
        if (!spriteHasBeenSet)
        {
            _spriteRenderer.sprite = _sprite;
        }        
        _defaultColour = _spriteRenderer.color;
    }

    public void UpdateSprite(Sprite sprite)
    {   
        _spriteRenderer.sprite = sprite;
        spriteHasBeenSet = true;
    }

    public void TintSprite(Color colour)
    {
        StartCoroutine(TintSpriteForDuration(colour, 0.2f)); //TODO: Remove hard coding on the duration parameter
    }

    private IEnumerator TintSpriteForDuration(Color colour, float duration) 
    {
        _spriteRenderer.color = colour;
        yield return new WaitForSeconds(duration);
        float time = 0f;
        while ((time += Time.deltaTime) < duration)
        {
            _spriteRenderer.color = Color.Lerp(colour, _defaultColour, time / duration); //TODO: Use an animation curve instead of basic time/duration
           yield return null;
        }
        _spriteRenderer.color = _defaultColour;
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
    }

    public void Show()
    {
        _spriteRenderer.enabled = true;
    }
}
