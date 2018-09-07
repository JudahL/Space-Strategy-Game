using Zenject;
using UnityEngine;

public class ParticleBurstSpawner : MonoBehaviour
{
    private ParticleBurst.Pool _memoryPool;

    [Inject]
    public void Construct(ParticleBurst.Pool memoryPool)
    {
        _memoryPool = memoryPool;
    }

    public void SpawnBurst(Transform target)
    {
        _memoryPool.Spawn(target);
    }	
}