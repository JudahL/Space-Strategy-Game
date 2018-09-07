using UnityEngine;
using Zenject;

public class BattleSelectionArrow : MonoBehaviour
{
    [SerializeField]
    private Vector2 _offsetFromTarget = new Vector2(0f, 100f);
    
    private Camera _targetCamera;
    private UILerpComponent _lerpComponent;

    private Vector2 offscreenPosition;

    [Inject]
    public void Construct(Camera targetCam, UILerpComponent lerpComponent)
    {
        _targetCamera = targetCam;
        _lerpComponent = lerpComponent;
    }

    private void Start()
    {
        if (!_targetCamera)
        {
            _targetCamera = Camera.main;
            Debug.Log("No camera has been set, using Camera.main");
        }

        offscreenPosition = _targetCamera.WorldToScreenPoint(transform.position);
    }

    public void OnSelection(GameObject target)
    {
        Vector2 targetPosition = _targetCamera.WorldToScreenPoint(target.transform.position);
        Vector2 arrowPosition = targetPosition + (target.transform.lossyScale.y * _offsetFromTarget);


        float rotationAngleZ = Mathf.Rad2Deg * Mathf.Atan2(targetPosition.y - arrowPosition.y, targetPosition.x - arrowPosition.x);
        Quaternion rotation = Quaternion.Euler(0, 0, rotationAngleZ + 90f);

        transform.rotation = rotation;

        _lerpComponent.UpdatePositionWithOffsetRotation(arrowPosition, rotation);
    }

    public void ClearTarget()
    {
        _lerpComponent.UpdatePosition(offscreenPosition);
    }
}
