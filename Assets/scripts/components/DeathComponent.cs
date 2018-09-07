using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    [SerializeField]
    private GameplayEventStandard _deathEvent;
    [SerializeField]
    private ParticleBurstTrigger _onDeathEffectTrigger;

    public void TriggerDeath()
    {
        _onDeathEffectTrigger.SpawnBurst(transform);
        _deathEvent.TriggerEvent(gameObject.GetInstanceID());
    }	
}
