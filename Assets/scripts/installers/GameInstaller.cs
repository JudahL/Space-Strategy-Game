using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject _healthBarPrefab;
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private GameObject _floatingTextPrefab;
    [SerializeField]
    private GameObject _targetArrow;

    [Header("Parent Groups")]
    [SerializeField]
    private RectTransform _healthBarParent;
    [SerializeField]
    private RectTransform _floatingTextParent;
    [SerializeField]
    private RectTransform _targetArrrowParent;

    [Header("Scriptable Objects")]
    [SerializeField]
    private TargetSystem _targetSystem;

    [Header("Events")]
    [SerializeField]
    private GameplayEventGameObject _objectSelectedEvent;
    [SerializeField]
    private GameplayEventGameObject _targetAddedEvent;
    [SerializeField]
    private GameplayEventGameObject _targetRemovedEvent;
    [SerializeField]
    private GameplayEventStandard _onNewTurnEvent;
    [SerializeField]
    private GameplayEventCombatActionDetails _onCombatActionEvent;
    [SerializeField]
    private GameplayEventStatBuffDetails _statBuffEvent;
    [SerializeField]
    private GameplayEventAuraEffectDetails _auraEffectEvent;

    [Header("Setups")]
    [SerializeField]
    private CombatActionFloatingTextSetup _combatActionFloatTextSetup;

    public override void InstallBindings()
    {
        Container.Bind<GameInstaller>().FromInstance(this);
        Container.Bind<TargetSystem>().FromInstance(_targetSystem).WhenInjectedInto<PlayerSelectionComponent>();
        
        Container.BindInstance(Camera.main).CopyIntoAllSubContainers();
        Container.BindInstance(_combatActionFloatTextSetup);

        /**
         *      Events :
         */
        Container.BindInstance(_objectSelectedEvent).WithId(2).CopyIntoAllSubContainers();
        Container.BindInstance(_targetAddedEvent).WithId(0).CopyIntoAllSubContainers();
        Container.BindInstance(_targetRemovedEvent).WithId(1).CopyIntoAllSubContainers(); 
        Container.BindInstance(_onCombatActionEvent).CopyIntoAllSubContainers();
        Container.BindInstance(_onNewTurnEvent).WhenInjectedInto<Attachment>().CopyIntoAllSubContainers();
        Container.BindInstance(_onNewTurnEvent).WhenInjectedInto<Aura>().CopyIntoAllSubContainers();
        Container.BindInstance(_statBuffEvent).CopyIntoAllSubContainers();
        Container.BindInstance(_auraEffectEvent).CopyIntoAllSubContainers();

        /**
         *      Factories :
         */
        Container.BindFactory<float, Transform, Vector3, int, HealthBarUI, HealthBarUI.Factory>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<HealthBarInstaller>(_healthBarPrefab)
            .UnderTransform(_healthBarParent);

        Container.BindMemoryPool<Projectile, Projectile.Pool>()
            .WithInitialSize(5)
            .FromSubContainerResolve()
            .ByNewContextPrefab(_projectilePrefab);

        Container.BindMemoryPool<TargetSelectionArrow, TargetSelectionArrow.Pool>()
            .WithInitialSize(5)
            .FromSubContainerResolve()
            .ByNewContextPrefab(_targetArrow)
            .UnderTransform(_targetArrrowParent);

        Container.BindFactory<AuraEffect[], int, UniqueAuraType, Aura, Aura.Factory>().CopyIntoAllSubContainers();
        Container.Bind<AuraFactory>().AsSingle().CopyIntoAllSubContainers();

        Container.BindFactory<CombatActionEffectSettings, IOffensiveStats, Transform, int, CombatActionEffect, CombatActionEffect.Factory>().CopyIntoAllSubContainers();

        Container.BindMemoryPool<FloatingText, FloatingText.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_floatingTextPrefab)
            .UnderTransform(_floatingTextParent);
    }	
}
