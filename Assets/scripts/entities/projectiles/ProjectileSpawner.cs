using UnityEngine;
using Zenject;

public class ProjectileSpawner : MonoBehaviour
{
    private Projectile.Pool _pool;

    [Inject]
    public void Construct(Projectile.Pool pool)
    {
        _pool = pool;
    }

    public void SpawnProjectile(ProjectileDetails projDetails)
    {
        _pool.Spawn(projDetails);
    }
}
