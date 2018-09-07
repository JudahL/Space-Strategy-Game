using System.Collections;
using UnityEngine;
using Zenject;

public class UILerpComponent : MonoBehaviour
{
    [SerializeField]
    protected float _lerpDuration = 1f;
    [SerializeField]
    protected Vector2 _lerpOffset = new Vector2 (0f, 50f);
    [SerializeField]
    protected bool useAnchoredPos = false;
    
    [InjectOptional]
    private RectTransform _transform;

    private Vector2 _defaultPos;
    private Vector2 _activatedPos;

    private bool _isAtDownPos = true;    

    private IEnumerator _lerpCoroutine;

    private void Start()
    {
        if (_transform == null)
        {
            _transform = GetComponent<RectTransform>();
            _defaultPos = _transform.anchoredPosition;
            _activatedPos = _defaultPos + _lerpOffset;
        }       
    }

    public void UpdatePosition(Vector2 position)
    {
        _defaultPos = position;
        _activatedPos = position + _lerpOffset;
        _isAtDownPos = true;
        SetToDefaultPos();
    }

    public void UpdatePositionWithOffsetRotation(Vector3 position, Quaternion rot)
    {
        _defaultPos = position;
        _activatedPos = position + (rot*_lerpOffset);
        _isAtDownPos = true;
        SetToDefaultPos();
    }

    public void Toggle()
    {
        if (_isAtDownPos)
            StartLerp(_defaultPos, _activatedPos);
        else
            StartLerp(_activatedPos, _defaultPos);
    }    

    public void SetToDefaultPos()
    {
        StartLerp(_activatedPos, _defaultPos);
    }

    public void SetToActivatedPos()
    {
        StartLerp(_defaultPos, _activatedPos);
    }

    private void StartLerp(Vector2 currentPos, Vector2 destinationPos)
    {
        ResetCoroutine();
        _lerpCoroutine = LerpPosition(currentPos, destinationPos);
        StartCoroutine(_lerpCoroutine);
    }

    private void ResetCoroutine()
    {
        if (_lerpCoroutine != null)
        {
            StopCoroutine(_lerpCoroutine);
        }
    }

    private IEnumerator LerpPosition(Vector2 currentPos, Vector2 destinationPos)
    {
        float time = 0;
        while ((time += Time.deltaTime) < _lerpDuration)
        {
            float t = time / _lerpDuration;
            t = t * t * t * (t * (t * 6 - 15) + 10); //Smootherstep
            SetTransformPosition(Vector2.Lerp(currentPos, destinationPos, t));
            yield return null;
        }
        SetTransformPosition(destinationPos);

        _isAtDownPos = destinationPos == _defaultPos ? true : false;

        OnComplete();
    }   

    private void SetTransformPosition(Vector2 destinationPosition)
    {
        if (useAnchoredPos)
            _transform.anchoredPosition = destinationPosition;
        else
            _transform.position = destinationPosition;
    }

    protected virtual void OnComplete() { }
}
