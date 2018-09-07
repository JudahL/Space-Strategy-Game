using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{ 
    private ProjectileDetails _details;
    private Transform _tr;
    private SpriteDisplayComponent _spriteComponent;
    private PositionChangeOverTimeComponent _positionChangeComponent;
    private Pool _memoryPool;
    private ParticleBurstTrigger _particleTrigger;

    [Inject]
    public void Construct(Transform tr, SpriteDisplayComponent spriteComponent, PositionChangeOverTimeComponent positionChangeComponent, Pool memoryPool, ParticleBurstTrigger particleTrigger)
    {
        _spriteComponent = spriteComponent;
        _positionChangeComponent = positionChangeComponent;
        _tr = tr;
        _memoryPool = memoryPool;
        _particleTrigger = particleTrigger;
    }

    private void UpdateDetails(ProjectileDetails details)
    {
        _details = details;

        _tr.position = _details.CasterTransform.position;
        _tr.localScale = _details.Scale;
        _spriteComponent.UpdateSprite(_details.Artwork);
    }

    private void Activate()
    {
        _positionChangeComponent.StartPositionChange(GetStartPosition(), GetTargetPosition(), _details.Speed, OnProjectileHit);
    }

    private Vector3 GetStartPosition()
    {
        Vector3 pos = _details.CasterTransform.position;
        pos.z = 0.00001f;
        return pos;
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 pos = _details.TargetTransform.position;
        pos.z = -0.00001f;
        return pos;
    }

    private Vector3 GetOffscreenPosition() //Used when the target has evaded the projectile
    {
        return GetTargetPosition() + (GetTargetPosition()-GetStartPosition());
    }

    private void OnProjectileHit()
    {
        _details.OnHitCommand.Execute();

        if (!_details.TargetHasEvaded)
        {
            OnConfirmedHit();
        } 
        else
        {
            OnEvadedHit();
        }
    }

    private void OnEvadedHit()
    {
        _positionChangeComponent.StartPositionChange(GetTargetPosition(), GetOffscreenPosition(), _details.Speed, Despawn);
    }

    private void OnConfirmedHit()
    {
        _particleTrigger.SpawnBurst(_tr);
        Despawn();
    }

    private void Despawn()
    {
        _memoryPool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<ProjectileDetails, Projectile>
    {
        protected override void Reinitialize(ProjectileDetails details, Projectile projectile)
        {
            projectile.UpdateDetails(details);
            projectile.Activate();
        }
    }   
}
