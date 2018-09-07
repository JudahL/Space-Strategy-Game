using UnityEngine;

[CreateAssetMenu(fileName = "Attachment", menuName = "Gameplay/Attachment")]
public class AttachmentSettings : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private int _cooldown;
    [SerializeField]
    private CombatActionEffectSettings[] _actions;

    public string Name                                      { get { return _name;        } }
    public string Description                               { get { return _description; } }
    public Sprite Icon                                      { get { return _icon;        } }
    public int Cooldown                                     { get { return _cooldown;    } }
    public CombatActionEffectSettings[] ActivationEffects   { get { return _actions;     } }
}

[System.Serializable]
public struct CombatActionEffectSettings
{
    public string Description;
    public bool IsHealing;
    public float PowerRatio;
    public int MaximumTargets;
    public TargetSystem TargetSystemToUse;
    public TargetSearchType ValidTargets;
    public AttachmentVFX VFX;
    public AuraSettings AuraToApply;
}
