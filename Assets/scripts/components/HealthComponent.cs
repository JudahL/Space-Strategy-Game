using UnityEngine;
using Zenject;

public class HealthComponent : MonoBehaviour, IHealthComponent
{

    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _maxHealth;

    private GameplayEventInt _healthChangeEvent;
    private IDefensiveStats _stats;
    private DeathComponent _deathComponent;

    [Inject]
    public void Construct(IDefensiveStats stats, GameplayEventInt healthChangeEvent, int startingHealth, DeathComponent deathComponent)
    {
        _stats = stats;
        _healthChangeEvent = healthChangeEvent;
        SetHealth(startingHealth);
        _deathComponent = deathComponent;
    }

    private void SetHealth(int health)
    {
        _currentHealth = health;
        _maxHealth = health;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public int TakeDamage(int amount, DamageType type = DamageType.Standard)
    {
        int damage = amount;

        if (type != DamageType.ArmorPiercing)
        {
            damage -= _stats.Armor;
            damage = damage < 0 ? 0 : damage;
        }        

        _currentHealth -= damage;
        ClampHealth();

        TriggerHealthChangeEvent();

        if (_currentHealth == 0)
        {
            _deathComponent.TriggerDeath();
        }

        return damage;
    }

    public int Heal(int amount)
    {
        _currentHealth += amount;
        ClampHealth();

        TriggerHealthChangeEvent();

        return amount;
    }

    private void TriggerHealthChangeEvent() {
        _healthChangeEvent.TriggerEvent(gameObject.GetInstanceID(), _currentHealth);
    }

    //Keeps the current health between 0 and the maximum health defined by maxHealth;
    private void ClampHealth()
    {
        _currentHealth = _currentHealth > _maxHealth ? _maxHealth : _currentHealth;
        _currentHealth = _currentHealth < 0 ? 0 : _currentHealth;
    }

}
