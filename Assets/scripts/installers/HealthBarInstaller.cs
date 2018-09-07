using UnityEngine.UI;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthBarUI), typeof(BarFillComponent))]
public class HealthBarInstaller : MonoInstaller
{
    [SerializeField]
    private RectTransform _barFillTr;
    [SerializeField]
    private Text _barText;
    [SerializeField]
    private RectTransform _iconParent;
    [SerializeField]
    private GameObject _displayIconPrefab;
    [SerializeField]
    private StatusIconManager _iconManager;
    
    private float _health = 1;
    private Transform _targetTransform;
    private Vector3 _positionOffset;
    private int _observingID = 0;

    [Inject]
    public void Construct(float health, Transform targetTransform, Vector3 positionOffset, int observingID)
    {
        _health = health;
        _targetTransform = targetTransform;
        _positionOffset = positionOffset;
        _observingID = observingID;
    }

    public override void InstallBindings()
    {
        /**
         *      Instances :
         */      
        Container.BindInstance(_barText);
        Container.BindInstance(_barFillTr).WhenInjectedInto<BarFillComponent>();
        Container.BindInstance(_health).WhenInjectedInto<HealthBarUI>();
        Container.BindInstance(_targetTransform).WhenInjectedInto<HealthBarUI>();
        Container.BindInstance(_positionOffset).WhenInjectedInto<HealthBarUI>();
        Container.BindInstance(_observingID).WhenInjectedInto<GameplayEventObserverInt>();
        Container.BindInstance(_observingID).WhenInjectedInto<GameplayEventObserverStandard>();
        Container.BindInstance(_observingID).WhenInjectedInto<GameplayEventObserverStatBuffDetails>();
        Container.BindInstance(_observingID).WhenInjectedInto<GameplayEventObserverAuraEffectDetails>();
        Container.BindInstance(GetComponent<BarFillComponent>());
        Container.BindInstance(GetComponent<HealthBarUI>());
        Container.BindInstance(_iconManager);

        /**
         *      Factories :
         */      
        Container.BindMemoryPool<DisplayIcon, DisplayIcon.Pool>()
            .WithInitialSize(1)
            .FromComponentInNewPrefab(_displayIconPrefab)
            .UnderTransform(_iconParent);
    }
}
