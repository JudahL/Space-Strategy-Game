using UnityEngine;

[CreateAssetMenu(fileName = "Aura", menuName = "Auras/Aura")]
public class AuraSettings : ScriptableObject
{
    public int Duration;
    public UniqueAuraType UniqueType;
    public AuraEffectSettings[] AuraEffects;
}
