using UnityEngine;
using Zenject;
using System.Collections;

public class ParticleBurst : MonoBehaviour
{
    [SerializeField]
    private EmitParticles _particleEmitter;

    private Pool _memoryPool;

    [Inject]
    public void Construct(Pool memoryPool)
    {
        _memoryPool = memoryPool;
    }

    public void Burst(Transform target)
    {
        transform.position = target.position;
        Burst();
    }

    public void Burst()
    {
        _particleEmitter.Emit();
        StartCoroutine(DespawnAfter());
    }

    private IEnumerator DespawnAfter()
    {
        yield return new WaitForSeconds(3f);
        _memoryPool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<Transform, ParticleBurst>
    {
        protected override void Reinitialize(Transform target, ParticleBurst particles)
        {
            particles.Burst(target);
        }
    }
}