using UnityEngine;
using Zenject;

[RequireComponent(typeof(Ship))]
public class ShipInstaller : MonoInstaller
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private GameplayEventInt _healthChangeEvent;
    [SerializeField]
    private Color _damageTint;
    [SerializeField]
    private Color _evadedTint;

    private ShipInfo _shipInfo;
    private Vector3 _position;
    private int _teamId;

    [Inject]
    public void Construct(ShipInfo info, Vector3 position, int teamId)
    {
        _shipInfo = info;
        _position = position;
        _teamId = teamId;
    }

    public override void InstallBindings()
    {
        /**
         *      Entities :
         */      
        Ship ship = GetComponent<Ship>();
        Container.BindInterfacesAndSelfTo<Ship>().FromInstance(ship);


        /**
         *      Components :
         */
        Container.Bind<IHealthComponent>().FromInstance(GetComponent<IHealthComponent>());
        Container.BindInstance(transform);
        Container.BindInstance(GetComponent<AttachmentHolderComponent>());
        Container.BindInstance(_spriteRenderer);
        Container.BindInstance(GetComponent<DeathComponent>());
        Container.BindInstance(GetComponent<SpriteDisplayComponent>());
        Container.BindInstance(GetComponent<AuraHolderComponent>());

        /**
         *      Ship Info :
         */
        Container.BindInstance(_shipInfo);
        Container.BindInstance(_shipInfo.Artwork);
        Container.BindInstance(_shipInfo.Health).WhenInjectedInto<IHealthComponent>();
        Container.BindInstance(_shipInfo.Attachments);

        /**
          *      Ship Tints :
          */
        Container.BindInstance(_damageTint).WhenInjectedInto<IDamageHandlerComponent>();
        Container.BindInstance(_evadedTint).WhenInjectedInto<IDamageHandlerComponent>();

        /**
         *      Factories :
         */
        Container.BindFactory<AttachmentSettings, Attachment, Attachment.Factory>();

        /**
         *      IDs :
         */
        Container.BindInstance(gameObject.GetInstanceID()).WhenInjectedInto<GameplayEventInt>();
        Container.BindInstance(gameObject.GetInstanceID()).WhenInjectedInto<GameplayEventObserverStandard>();
        Container.BindInstance(_teamId).WhenInjectedInto<Attachment>();

        /**
         *      Events :
         */
        Container.Bind<GameplayEventInt>().FromInstance(_healthChangeEvent).WhenInjectedInto<IHealthComponent>();
    }

    public override void Start()
    {
        base.Start();

        ///Set local position of the ship
        transform.localPosition = _position; //TODO: Potentially add new component that controls ship position
    }
}
