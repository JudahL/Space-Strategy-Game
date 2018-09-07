using UnityEngine;

[CreateAssetMenu(fileName = "TargetSystem", menuName = "Gameplay/TargetSystems/Raycast")]
public class RaycastTargetSystem : TargetSystem
{
    public void OnInput(Vector2 inputVector)
    {
        if (_currentClient == null) return;

        if (_isSearching)
        {
            CastRay(inputVector);
        }
    }

    private void CastRay(Vector2 rayPosition)
    {
        Vector2 rayPos = Camera.main.ScreenToWorldPoint(rayPosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(rayPos, _layerMask);

        if (hitCollider)
        {
            GameObject target = hitCollider.gameObject;
            AddTarget(target);
        }
    }

    protected override void AllTargetsSet()
    {
        base.AllTargetsSet();
    }
}
