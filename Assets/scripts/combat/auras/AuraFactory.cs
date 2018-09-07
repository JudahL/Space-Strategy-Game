using Zenject;

public class AuraFactory  : IFactory<AuraSettings, Aura>
{
    private readonly DiContainer _container;
    private readonly Aura.Factory _auraFactory;

    public AuraFactory(DiContainer container, Aura.Factory auraFactory)
    {
        _container = container;
        _auraFactory = auraFactory;
    }

    public Aura Create(AuraSettings type)
    {
        AuraEffect[] effects = new AuraEffect[type.AuraEffects.Length];

        for (int i = 0; i < type.AuraEffects.Length; i++)
        {
            effects[i] = type.AuraEffects[i].BuildAuraEffect(_container);
        }

        return _auraFactory.Create(effects, type.Duration, type.UniqueType);
    }
}
