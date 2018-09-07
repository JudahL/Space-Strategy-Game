using System.Collections.Generic;
using UnityEngine;

public class AuraHolderComponent : MonoBehaviour
{
    private List<Aura> _nonUniqueAuras = new List<Aura>();
    private Dictionary<UniqueAuraType, Aura> _uniqueAuras = new Dictionary<UniqueAuraType, Aura>();

    public void AddAura(Aura newAura)
    {
        if (newAura.UniqueType != UniqueAuraType.None)
        {
            TryAddUniqueAura(newAura);
        } 
        else
        {
            AddNonUniqueAura(newAura);
        }        
    }

    private void AddNonUniqueAura(Aura newAura)
    {
        newAura.ApplyAura(gameObject);
        _nonUniqueAuras.Add(newAura);
    }

    private void TryAddUniqueAura(Aura newAura)
    {
        if (_uniqueAuras.ContainsKey(newAura.UniqueType))
        {
            if (_uniqueAuras[newAura.UniqueType].PowerValue < newAura.PowerValue)
            {
                RemoveUniqueAura(newAura.UniqueType);
                AddUniqueAura(newAura);
            }
        } 
        else
        {
            AddUniqueAura(newAura);
        }       
    }

    private void AddUniqueAura(Aura newAura)
    {
        newAura.ApplyAura(gameObject);
        _uniqueAuras.Add(newAura.UniqueType, newAura);
    }

    private void RemoveUniqueAura(UniqueAuraType uniqueType)
    {
        _uniqueAuras[uniqueType].RemoveAura();
        _uniqueAuras.Remove(uniqueType);
    }
}


public enum UniqueAuraType
{
    None,
    Fire,
    Breach
}