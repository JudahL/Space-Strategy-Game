using System.Collections;
using UnityEngine;

public class ScaleRectTransformByCurve : MonoBehaviour, IAnimatedByCurve
{
    [SerializeField]
    private float _duration;
    [SerializeField]
    private AnimationCurve _curve;
    [SerializeField]
    private Transform _tr;
    
    public void Animate()
    {
        StartCoroutine(ScaleByCurve(_curve, _duration));
    }

    public void AnimateByCustomParams(AnimationCurve curve, float duration)
    {
        StartCoroutine(ScaleByCurve(curve, duration));
    }

    private IEnumerator ScaleByCurve(AnimationCurve curve, float duration)
    {
        float time = 0f;
        while ((time += Time.deltaTime) < duration)
        {
            float t = time / duration;
            _tr.localScale = new Vector2(curve.Evaluate(t), curve.Evaluate(t));
            yield return null;
        }
        _tr.localScale = new Vector2(curve.Evaluate(1f), curve.Evaluate(1f));
    }
}
