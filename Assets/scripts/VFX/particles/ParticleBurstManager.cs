using UnityEngine;

public class ParticleBurstManager : MonoBehaviour
{
    [SerializeField]
    private SpawnType[] _spawnTypes;

    private void Start()
    {
        for (int i = 0; i < _spawnTypes.Length; i++)
        {
            _spawnTypes[i]._trigger.Setup(i, this);
        }
    }

    public void SpawnBurst(int index, Transform target)
    {
        _spawnTypes[index]._spawner.SpawnBurst(target);
    }

    [System.Serializable]
    private struct SpawnType
    {
        public ParticleBurstTrigger _trigger;
        public ParticleBurstSpawner _spawner;
    }
}
