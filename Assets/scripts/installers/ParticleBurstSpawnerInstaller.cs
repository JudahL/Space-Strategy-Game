using UnityEngine;
using Zenject;

public class ParticleBurstSpawnerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _particleBurstPrefab;
    [SerializeField]
    private int _initialSpawnAmount = 0;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<ParticleBurst, ParticleBurst.Pool>()
            .WithInitialSize(_initialSpawnAmount)
            .FromComponentInNewPrefab(_particleBurstPrefab);
    }
}
