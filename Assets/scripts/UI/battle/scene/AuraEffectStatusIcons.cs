using UnityEngine;
using Zenject;

public class AuraEffectStatusIcons : MonoBehaviour
{
    [SerializeField]
    private AuraEffectStatusIcon[] _auraEffectSprites;

    private StatusIconManager _iconManager;

    [Inject]
    public void Construct(StatusIconManager iconManager)
    {
        _iconManager = iconManager;
    }

    public void OnAuraEffect(AuraEffectDetails details)
    {
        Sprite sprite = GetSprite(details.Type);

        if (sprite != null)
        {
            if (details.IsApplying)
            {
                _iconManager.AddIcon(sprite);
            } 
            else
            {
                _iconManager.RemoveIcon(sprite);
            }
        }        
    }

    private Sprite GetSprite(AuraEffectType typeToSearchFor)
    {
        for (int i = 0; i < _auraEffectSprites.Length; i++)
        {
            if (_auraEffectSprites[i].Type == typeToSearchFor)
            {
                return _auraEffectSprites[i].IconSprite;
            }
        }

        return null;
    }

    [System.Serializable]
    private struct AuraEffectStatusIcon
    {
        public AuraEffectType Type;
        public Sprite IconSprite;
    }
}
