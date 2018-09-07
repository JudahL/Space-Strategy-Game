using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCurvePreset", menuName = "Presets/AnimationCurve")]
public class AnimationCurvePreset : ScriptableObject
{
    [SerializeField]
    private AnimationCurve _animationCurve;

    public AnimationCurve AnimationCurve { get { return _animationCurve; } }
}
