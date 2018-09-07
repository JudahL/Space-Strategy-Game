using UnityEngine;
using Zenject;

public class TargetSelectionArrow : MonoBehaviour
{
    [SerializeField]
    private Vector2 _offsetFromTarget = new Vector2(0f, 100f);

    private UILerpComponent _lerpComponent;
    private RectTransform _tr;
    private Camera _targetCamera;

    [Inject]
    public void Construct(UILerpComponent lerpComponent, RectTransform tr, Camera targetCamera)
    {
        _lerpComponent = lerpComponent;
        _tr = tr;
        _targetCamera = targetCamera;
    }

    public void UpdateArrow(Transform targetTransform)
    {
        Vector2 targetPosition = _targetCamera.WorldToScreenPoint(targetTransform.position);
        Vector2 arrowPosition = targetPosition + (_offsetFromTarget * targetTransform.lossyScale.y);

        _lerpComponent.UpdatePosition(arrowPosition);

        SetArrowRotation(arrowPosition, targetPosition);
    }

    private void SetArrowRotation(Vector2 arrowPos, Vector2 targetPos)
    {
        float rotation = Mathf.Rad2Deg * Mathf.Atan2(targetPos.y - arrowPos.y, targetPos.x - arrowPos.x);
        _tr.rotation = Quaternion.Euler(0, 0, rotation + 90f);
    }

    public class Pool : MonoMemoryPool<Transform, TargetSelectionArrow>
    {
        protected override void Reinitialize(Transform targetTransform, TargetSelectionArrow arrow)
        {
            arrow.UpdateArrow(targetTransform);
        }
    }
}
