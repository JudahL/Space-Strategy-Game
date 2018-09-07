using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Ship")]
public class ShipSettings : ScriptableObject
{
    public string ShipName;
    public string Description;

    public Sprite Image;

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _crit;
    [SerializeField]
    private int _evade;
    [SerializeField]
    private int _armor;

    [SerializeField]
    private float _healthIncrease;
    [SerializeField]
    private float _damageIncrease;
    [SerializeField]
    private float _critIncrease;
    [SerializeField]
    private float _evadeIncrease;
    [SerializeField]
    private float _armorIncrease;

    public int GetHealth(int level)
    {
        return _health + Mathf.FloorToInt(level * _healthIncrease);
    }

    public int GetDamage(int level)
    {
        return _damage + Mathf.FloorToInt(level * _damageIncrease);
    }

    public int GetCrit(int level)
    {
        return _crit + Mathf.FloorToInt(level * _critIncrease);
    }

    public int GetEvade(int level)
    {
        return _evade + Mathf.FloorToInt(level * _evadeIncrease);
    }

    public int GetArmor(int level)
    {
        return _armor + Mathf.FloorToInt(level * _armorIncrease);
    }
}
