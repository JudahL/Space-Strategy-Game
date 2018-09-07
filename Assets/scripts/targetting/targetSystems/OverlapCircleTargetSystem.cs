using UnityEngine;

[CreateAssetMenu(fileName = "TargetSystem", menuName = "Gameplay/TargetSystems/Circle")]
public class OverlapCircleTargetSystem : TargetSystem
{
    [SerializeField]
    private float defaultRadius;
    [SerializeField]
    private Vector2 defaultCenter;

    protected override void OnBeginSearch()
    {
        if (_currentClient == null) return;

        if (_isSearching)
        {
            CheckCircle(defaultCenter, defaultRadius);
        }
    }

    private void CheckCircle(Vector2 circleCenter, float radius)
    {
        Vector2 rayPos = Camera.main.ViewportToWorldPoint(circleCenter);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(circleCenter, radius, _layerMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i])
            {
                GameObject target = hitColliders[i].gameObject;
                AddTarget(target);
            }
        }        
    }

    protected override void AllTargetsSet()
    {
        base.AllTargetsSet();
    }
}
