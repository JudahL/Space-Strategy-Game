using UnityEngine;

public interface IAnimatedByCurve
{
    void Animate();
    void AnimateByCustomParams(AnimationCurve curve, float duration);
}
