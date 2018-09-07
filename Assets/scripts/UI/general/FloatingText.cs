using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private RectTransform _tr;
    [SerializeField]
    private ScaleRectTransformByCurve _scaleByCurve;

    private Pool _memoryPool;

    [Inject]
    public void Construct(Pool memoryPool)
    {
        _memoryPool = memoryPool;
    }

    public void UpdateText(AnimatedTextInfo info, Vector2 screenPos)
    {
        _text.text = info.textInfo.text;
        _text.fontSize = info.textInfo.fontSize;
        _text.color = info.textInfo.colour;
        _tr.position = screenPos;

        StartCoroutine(WaitDuration(info.duration));

        if (info.curve != null)
        {
            _scaleByCurve.AnimateByCustomParams(info.curve.AnimationCurve, info.duration);
        }        
    }

    private IEnumerator WaitDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        _memoryPool.Despawn(this);
    }

    public bool IsEnabled()
    {
        return _text.enabled;
    }

    public class Pool : MonoMemoryPool<AnimatedTextInfo, Vector2, FloatingText>
    {
        protected override void Reinitialize(AnimatedTextInfo info, Vector2 pos, FloatingText instance)
        {
            instance.UpdateText(info, pos);
        }
    }
}
	
