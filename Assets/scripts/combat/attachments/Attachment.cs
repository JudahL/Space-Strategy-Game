using UnityEngine;
using Zenject;

[System.Serializable]
public class Attachment : IGameplayEventListener
{
    private AttachmentSettings _settings;

    private Cooldown _cooldown;
    private CombatActionEffect[] _activationEffects;

    private IOffensiveStats _stats;
    private Transform _ownerTr;
    private int _teamId;
    
    private CombatActionEffect.Factory _combatActionEffectFactory;

    public Attachment(AttachmentSettings settings, IOffensiveStats stats, Transform tr, int teamId, CombatActionEffect.Factory combatActionEffectFactory)
    {
        _settings = settings;
        _stats = stats;
        _ownerTr = tr;
        _teamId = teamId;
        _cooldown.Total = settings.Cooldown;
        _cooldown.Remaining = settings.Cooldown;
        _combatActionEffectFactory = combatActionEffectFactory;
    }

    [Inject]
    public void Initialize(GameplayEventStandard newTurnEvent)
    {
        newTurnEvent.RegisterListener(this);

        BuildCombatActionEffects();
    }

    private void BuildCombatActionEffects()
    {
        _activationEffects = new CombatActionEffect[_settings.ActivationEffects.Length];

        for (int i = 0; i < _settings.ActivationEffects.Length; i++)
        {
            _activationEffects[i] = _combatActionEffectFactory.Create(_settings.ActivationEffects[i], _stats, _ownerTr, _teamId); 
        }
    }

    public void OnEventTriggered(int teamId) /// Called at the start of a new turn
    {
        if (teamId == _teamId)
            _cooldown.Remaining--;
    }

    public AttachmentDetails SelectAttachment()
    {
        if (_cooldown.Remaining <= 0)
        {
            ResetTargets();
            for (int i = 0; i < _activationEffects.Length; i++)
            {
                _activationEffects[i].OnSelect();
            }
        }

        return GetDetails();
    }

    public void Activate()
    {
        if (_cooldown.Remaining <= 0)
        {
            ApplyEffects();
            _cooldown.Remaining = _cooldown.Total;
        }
    }

    public void Cancel()
    {
        ResetTargets();
    }

    public void TriggerGlobal()
    {
        if (_cooldown.Remaining <= 1)
        {
            _cooldown.Remaining = 1;
        }
    }

    public Cooldown GetCooldown()
    {
        return _cooldown;
    }

    public Sprite GetIcon()
    {
        return _settings.Icon;
    }

    private void ResetTargets()
    {
        for (int i = 0; i < _activationEffects.Length; i++)
        {
            _activationEffects[i].ResetTargets();
        }
    }

    private void ApplyEffects()
    {
        for (int i = 0; i < _activationEffects.Length; i++)
        {
            _activationEffects[i].ApplyEffects();
        }
    }
    

    private AttachmentDetails GetDetails()
    {
        return new AttachmentDetails(_settings.Name, BuildAndReturnDescription(), _settings.Cooldown, _activationEffects[0].MaximumTargets);
    }

    private string BuildAndReturnDescription()
    {
        return _settings.Description.Replace("@", CalculatePower().ToString()).Replace("#", _activationEffects[0].MaximumTargets.ToString());
    }

    private int CalculatePower()
    {
        return Mathf.RoundToInt(_activationEffects[0].PowerRatio * _stats.Damage);
    }

    public class Factory : PlaceholderFactory<AttachmentSettings, Attachment> { }
}
