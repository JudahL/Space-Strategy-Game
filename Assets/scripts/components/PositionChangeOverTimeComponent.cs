using Zenject;
using UnityEngine;
using System.Collections;

public class PositionChangeOverTimeComponent : MonoBehaviour
{
    private Transform _tr;

    [SerializeField]
    private bool _rotateToFaceTargetPosition;

    public delegate void Callback();
    private Callback _onHitCallback;

    [Inject]
    public void Construct(Transform tr)
    {
        _tr = tr;
    }

    public void StartPositionChange(Vector3 startingPosition, Vector3 targetPosition, float duration)
    {
        _onHitCallback = null;

        ChangePosition(startingPosition, targetPosition, duration);
    }

    public void StartPositionChange(Vector3 startingPosition, Vector3 targetPosition, float duration, Callback onHitCallback)
    {
        _onHitCallback = onHitCallback;

        ChangePosition(startingPosition, targetPosition, duration);
    }

    private void ChangePosition(Vector3 startingPosition, Vector3 targetPosition, float duration)
    {
        StartCoroutine(ChangePositionOverTime(startingPosition, targetPosition, duration));

        if (_rotateToFaceTargetPosition)
        {
            _tr.up = targetPosition - startingPosition;
        }
    }

    private IEnumerator ChangePositionOverTime(Vector3 startingPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;
        while ((time += Time.deltaTime) < duration)
        {
            float t = time / duration;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        if (_onHitCallback != null)
        {
            _onHitCallback();
        }
    }
}
