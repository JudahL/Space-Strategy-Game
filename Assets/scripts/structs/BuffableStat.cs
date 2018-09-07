using System.Collections.Generic;

public struct BuffableStat
{
    public Stat Type;

    private int _baseValue;
    private List<IStatModifier> _modifiers;

    public int Value
    {
        get
        {
            return GetValue();
        }
        private set
        {
            _baseValue = value;
        }
    }

    public int BuffedValue
    {
        get
        {
            return Value - _baseValue;
        }
    }


    public BuffableStat(int baseValue, Stat type)
    {
        _baseValue = baseValue;
        Type = type;
        _modifiers = new List<IStatModifier>();
    }

    private int GetValue()
    {
        int value = _baseValue;

        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i].ApplyModification(ref value);
        }

        return value;
    }

    public void AddModifier(IStatModifier modifier)
    {
        _modifiers.Add(modifier);
    }

    public void RemoveModifier(IStatModifier modifier)
    {
        _modifiers.Remove(modifier);
    }

    public static implicit operator int(BuffableStat stat)
    {
        return stat.Value;
    }

    public static implicit operator string(BuffableStat stat)
    {
        return stat.Value.ToString();
    }

    public static BuffableStat operator +(IStatModifier modifier, BuffableStat stat)
    {
        stat.AddModifier(modifier);
        return stat;
    }
}