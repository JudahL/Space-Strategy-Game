using UnityEngine;

[RequireComponent(typeof(IAnimatedByCurve), typeof(FloatingText))]
public class CurveAnimatedText : MonoBehaviour
{
    private IAnimatedByCurve _curveAnimator;
    private FloatingText _textUtility;

    private void Start()
    {
        _textUtility = GetComponent<FloatingText>();
        _curveAnimator = GetComponent<IAnimatedByCurve>();
    }

    public void AnimateText(TextInfo textInfo, float duration)
    {
        //_textUtility.UpdateText(textInfo, duration);
    }

    public void AnimateText(TextInfo textInfo, AnimationCurve curve, float duration, Vector2 location)
    {
        //_textUtility.UpdateText(textInfo, duration, location);
        _curveAnimator.AnimateByCustomParams(curve, duration);
    }

    public bool IsTextEnabled()
    {
        return _textUtility.IsEnabled();
    }
}
