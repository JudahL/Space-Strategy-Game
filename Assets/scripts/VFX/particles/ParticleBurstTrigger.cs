using UnityEngine;

[CreateAssetMenu(fileName = "ParticleBurstTrigger", menuName = "VFX/ParticleBurstTrigger")]
public class ParticleBurstTrigger : ScriptableObject
{
    private int _index;
    private ParticleBurstManager _manager;

    public void Setup(int index, ParticleBurstManager manager)
    {
        _index = index;
        _manager = manager;
    }

    public void SpawnBurst(Transform target)
    {
        _manager.SpawnBurst(_index, target);
    }
}
