using Zenject;
using UnityEngine;

public class Aura : IGameplayEventListener
{
    private AuraEffect[] _effects;
    private int _durationInTurns;
    private GameplayEventStandard _newTurnEvent;

    public UniqueAuraType UniqueType { get { return _uniqueAuraType; } }

    public int PowerValue { get { return _durationInTurns; } }

    public Aura(AuraEffect[] effects, int duration, UniqueAuraType uniqueType, GameplayEventStandard newTurnEvent)
    {
        _effects = effects;
        _durationInTurns = duration;
        _newTurnEvent = newTurnEvent;
        _uniqueAuraType = uniqueType;
    }

    public void ApplyAura(GameObject target)
    {
        _newTurnEvent.RegisterListener(this);

        ApplyEffects(target);
    }

    public void RemoveAura()
    {
        _newTurnEvent.UnregisterListener(this);

        RemoveEffects();
    }

    private void ApplyEffects(GameObject target)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            _effects[i].Apply(target);
        }
    }

    private void RemoveEffects()
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            _effects[i].Remove();
        }

        _effects = null;
    }

    public void OnEventTriggered(int senderId) /// Called at the start of a new turn
    {
        OnNewTurn();
    }

    private void OnNewTurn()
    {
        _durationInTurns--;

        if (_durationInTurns <= 0)
        {
            RemoveAura();
        } else
        {
            for (int i = 0; i < _effects.Length; i++)
            {
                _effects[i].OnNewTurn();
            }
        }
    }    

    public class Factory : PlaceholderFactory<AuraEffect[], int, UniqueAuraType, Aura> { }
    
    private UniqueAuraType _uniqueAuraType;
}