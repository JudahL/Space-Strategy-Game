using Zenject;
using UnityEngine;

public class ProjectileInstaller : MonoInstaller
{
    [SerializeField]
    private Sprite _placeholderSprite;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private SpriteDisplayComponent _spriteDisplayComponent;
    [SerializeField]
    private PositionChangeOverTimeComponent _positionChangeComponent;
    [SerializeField]
    private ParticleBurstTrigger _particleTrigger;

    public override void InstallBindings()
    {
        Container.BindInstance(transform);
        Container.BindInstance(_placeholderSprite);
        Container.BindInstance(_spriteRenderer);
        Container.BindInstance(_spriteDisplayComponent);
        Container.BindInstance(_positionChangeComponent);
        Container.BindInstance(GetComponent<Projectile>());
        Container.BindInstance(_particleTrigger);
    }

    public override void Start()
    {
        base.Start();
    }
}
