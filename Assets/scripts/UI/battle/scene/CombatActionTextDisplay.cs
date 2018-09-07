using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class CombatActionTextDisplay : FloatingTextDisplay
{
    [SerializeField]
    private int _textYOffset = 75;
    [SerializeField]
    private int _textYSplit = 50;

    private Dictionary<CombatActionType, AnimatedTextInfo> _textInfo;
    private CombatActionFloatingTextSetup _textSetup;

    public Dictionary<Transform, int> transformCounts = new Dictionary<Transform, int>();

    [Inject]
    public void Construct(CombatActionFloatingTextSetup textSetup)
    {
        _textSetup = textSetup;
    }

    private void Start()
    {
        LoadTextSetups();
    }

    private void LoadTextSetups()
    {
        _textInfo = _textSetup.GetSetupDictionary();
    }

    public void OnCombatAction(CombatActionDetails details)
    {
        AnimatedTextInfo info = _textInfo[details.Type];
        info.textInfo.text = info.textInfo.text.Replace("@", details.Value.ToString());

        CheckTarget(details.TargetTransform);

        NewCustomDisplay(info, GetFloatingTextPosition(details.TargetTransform));

        StartCoroutine(WaitForTextDuration(info.duration, details.TargetTransform));
    }

    private Vector2 GetFloatingTextPosition(Transform target)
    {
        return Camera.main.WorldToScreenPoint(target.position) + new Vector3(0, (_textYOffset + GetTextPositionMultiplier(target)) * target.lossyScale.y , 0);
    }

    private void CheckTarget(Transform target)
    {
        if (transformCounts.ContainsKey(target))
        {
            transformCounts[target]++;
        }
        else
        {
            transformCounts.Add(target, 1);
        }
    }

    private IEnumerator WaitForTextDuration(float duration, Transform target)
    {
        yield return new WaitForSeconds(duration);
        if (transformCounts.ContainsKey(target))
        {
            transformCounts[target]--;

            if (transformCounts[target] < 0)
            {
                transformCounts[target] = 0;
            }
        }
    }

    private float GetTextPositionMultiplier(Transform target)
    {
        return (transformCounts[target] - 1) * _textYSplit;
    }
}


// TODO : PUT IN SEPERATE FILE
[System.Serializable]
public struct TextInfo
{
    public string text;
    public int fontSize;
    public Color colour;

    public TextInfo(string text, int fontSize, Color colour)
    {
        this.text = text;
        this.fontSize = fontSize;
        this.colour = colour;
    }
}
[System.Serializable]
public struct AnimatedTextInfo
{
    public TextInfo textInfo;
    public AnimationCurvePreset curve;
    public float duration;

    public AnimatedTextInfo(TextInfo info, AnimationCurvePreset preset, float dur)
    {
        textInfo = info;
        curve = preset;
        duration = dur;
    }
}