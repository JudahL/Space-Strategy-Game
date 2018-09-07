using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class FloatingTextDisplay : MonoBehaviour
{
    [SerializeField]
    protected int _defaultFontSize = 28;
    [SerializeField]
    protected Color _defaultColor = Color.white;
    [SerializeField]
    protected float _defaultDuration = 2f;

    private FloatingText.Pool _floatingTextPool;

    [Inject]
    public void Construct(FloatingText.Pool pool)
    {
        _floatingTextPool = pool;
    }

    public void NewDisplay(string content)
    {
        _floatingTextPool.Spawn(new AnimatedTextInfo(new TextInfo(content, _defaultFontSize, _defaultColor), DEFAULT_ANIM_CURVE, _defaultDuration), Vector2.zero);
    }

    public void NewCustomDisplay(AnimatedTextInfo textInfo, Vector2 location)
    {
        _floatingTextPool.Spawn(textInfo, location);
    }
    
    private static readonly Vector2 DEFAULT_POSITION = Vector2.zero;
    private static readonly AnimationCurvePreset DEFAULT_ANIM_CURVE = null;
}