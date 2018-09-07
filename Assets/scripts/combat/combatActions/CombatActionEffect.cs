using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class CombatActionEffect : ITargetSystemClient
{
    /**
     *      Settings :
     */
    private bool _isHealing;
    private float _powerRatio;
    private int _maximumTargets;
    private TargetSystem _targetSystem;
    private TargetSearchType _searchType;
    private AttachmentVFX _vfx;
    private AuraSettings _auraType;

    /**
     *      Client :
     */
    private IOffensiveStats _stats;
    private Transform _ownerTr;
    private int _teamId;

    /**
     *      Other:
     */
    private GameplayEventGameObject _targetAddedEvent;
    private GameplayEventGameObject _targetRemovedEvent;
    private AuraFactory _auraFactory;

    private struct Target
    {
        public IDamageHandlerComponent damageHandlerComponent;
        public GameObject gameObject;

        public Target(IDamageHandlerComponent damageHandler, GameObject go)
        {
            damageHandlerComponent = damageHandler;
            gameObject = go;
        }
    }

    private List<Target> targets = new List<Target>();

    public float PowerRatio     { get { return _powerRatio;     } }
    public int MaximumTargets   { get { return _maximumTargets; } }

    public CombatActionEffect(CombatActionEffectSettings settings, IOffensiveStats stats, Transform ownerTr, int teamId, AuraFactory auraFactory,
                                [Inject(Id = 0)]GameplayEventGameObject targetAddedEvent, 
                                [Inject(Id = 1)]GameplayEventGameObject targetRemovedEvent)
    {
        /**
         *      Settings :
         */      
        _isHealing = settings.IsHealing;
        _powerRatio = settings.PowerRatio;
        _maximumTargets = settings.MaximumTargets;
        _targetSystem = settings.TargetSystemToUse;
        _searchType = settings.ValidTargets;
        _vfx = settings.VFX;
        _auraType = settings.AuraToApply;

        /**
         *      Client :
         */
        _stats = stats;
        _ownerTr = ownerTr;
        _teamId = teamId;

        /**
         *      Other:
         */
        _targetRemovedEvent = targetRemovedEvent;
        _targetAddedEvent = targetAddedEvent;
        _auraFactory = auraFactory;
    }

    public void OnSelect()
    {
        if (_targetSystem != null && _searchType != null)
        {
            _targetSystem.BeginSearch(this, _searchType.GetLayerMask(_teamId));
        } 
        else
        {
            bool allTargetsAcquired;
            TryAddTarget(_ownerTr.gameObject, out allTargetsAcquired);
        }
    }

    public void ApplyEffects()
    {
        if (targets == null)
        {
            Debug.Log("No targets found.");
            return;
        }

        for (int i = 0; i < targets.Count; i++)
        {
            ApplyEffect(targets[i].damageHandlerComponent);
        }

        ResetTargets();
    }

    private void ApplyEffect(IDamageHandlerComponent target)
    {
        bool hasCrit = CalculateAndReturnCrit();
        bool targetHasEvaded = target.HasEvaded();

        Aura aura = null;

        if (_auraType != null)
        {
            aura = _auraFactory.Create(_auraType);
        }

        DamageType type = _isHealing ? DamageType.Healing : DamageType.Standard;
        DamageDetails details = new DamageDetails(CalculateAndReturnDamage(hasCrit), type, hasCrit, targetHasEvaded, aura);

        AttachmentEffectCommand weaponEffectCommand = new AttachmentEffectCommand(target, details);

        if (_vfx != null)
        {
            _vfx.Activate(weaponEffectCommand, _ownerTr, target.EntityTransform, targetHasEvaded);
        } 
        else
        {
            weaponEffectCommand.Execute();
        }
    }

    private int CalculateAndReturnDamage(bool hasCrit)
    {
        if (hasCrit)
        {
            return Mathf.FloorToInt(_stats.Damage * _powerRatio) * 2;
        } 
        else
        {
            return Mathf.FloorToInt(_stats.Damage * _powerRatio);
        }
    }

    private bool CalculateAndReturnCrit()
    {
        return Random.Range(0, 100) < _stats.Crit;
    }

    public void ResetTargets()
    {
        if (targets != null) targets.Clear();
    }


    public bool TryAddTarget(GameObject target, out bool allTargetsAcquired)
    {
        allTargetsAcquired = false;

        IDamageHandlerComponent targetDamageHandler = target.GetComponent<IDamageHandlerComponent>();
        if (targetDamageHandler != null)
        {
            if (targets.Count >= _maximumTargets && targets.Count > 0)
            {
                _targetRemovedEvent.TriggerEvent(0, targets[0].gameObject);
                targets.RemoveAt(0);                
            }

            targets.Add(new Target(targetDamageHandler, target));

            _targetAddedEvent.TriggerEvent(0, target);

            return true;
        }

        return false;
    }

    public class Factory : PlaceholderFactory<CombatActionEffectSettings, IOffensiveStats, Transform, int, CombatActionEffect> { }
}

public interface CombatActionEffectClient : IOffensiveStats
{
    
}
