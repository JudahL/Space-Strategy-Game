using UnityEngine;

public interface IDamageHandlerComponent
{
    Transform EntityTransform { get; }
    void ProcessDamage(DamageDetails details);
    bool HasEvaded();
}
