using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileVFX", menuName = "VFX/Projectile")]
public class ProjectileVFX : AttachmentVFX
{
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private Vector3 _scale;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameplayEventProjectileDetails _event;

    protected override void OnActivate()
    {
        _event.TriggerEvent(0, new ProjectileDetails(_sprite, _scale, _speed, _casterTransform, _targetTransform, _effectCommand, _targetHasEvaded));
    }
}
