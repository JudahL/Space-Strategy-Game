using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private int _playerId;
    [SerializeField]
    private Transform _shipPrefab;
    [SerializeField]
    private Transform _shipParent;
    [SerializeField]
    private TargetSearchType _shipSearchType;
    [SerializeField]
    private GameplayEventAttachmentDetails _attachmentSelectedEvent;
    [SerializeField]
    private GameplayEventStandard _attachmentUnselectedEvent;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerId);

        Container.BindFactory<ShipInfo, Vector3, Ship, Ship.Factory>()
            .FromSubContainerResolve()
            .ByNewPrefab<ShipInstaller>(_shipPrefab)
            .UnderTransform(_shipParent);

        Container.BindInstance(_shipSearchType);

        Container.BindInstance(_attachmentSelectedEvent);
        Container.BindInstance(_attachmentUnselectedEvent);
    }
}
