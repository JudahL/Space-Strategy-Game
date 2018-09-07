public struct DamageDetails
{
    public DamageType Type;
    public bool IsCrit;
    public bool TargetHasEvaded;
    public Aura AuraToApply;
    
    public int Damage       { get { return _damage; } }
    
    public DamageDetails(int damage, DamageType type = DamageType.Standard, bool crit = false, bool hasEvaded = false, Aura aura = null)
    {
        _damage = damage;
        IsCrit = crit;
        Type = type;
        TargetHasEvaded = hasEvaded;
        AuraToApply = aura;
    }

    private int _damage;    
}
