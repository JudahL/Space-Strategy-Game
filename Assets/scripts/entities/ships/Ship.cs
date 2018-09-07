using UnityEngine;
using Zenject;

public class Ship : MonoBehaviour, IOffensiveStats, IDefensiveStats, IBuffableStats
{
    public ShipInfo Info                            { get { return _shipInfo; } }
    public Transform ShipTransform                  { get { return _shipTr; } }
    public AttachmentHolderComponent Attachments    { get { return _attachments; } }

    private GameplayEventStatBuffDetails _statBuffEvent;

    /**
     *      Stats:
     */
    public int Health            { get { return Info.Health;                } }
    public BuffableStat Damage   { get { return _stats[ (int)Stat.Damage ]; } }
    public BuffableStat Crit     { get { return _stats[ (int)Stat.Crit   ]; } }
    public BuffableStat Evade    { get { return _stats[ (int)Stat.Evade  ]; } }
    public BuffableStat Armor    { get { return _stats[ (int)Stat.Armor  ]; } }
    private BuffableStat[] _stats;

    [Inject]
    public void Construct(ShipInfo shipDetails, AttachmentHolderComponent attachments, Transform shipTr, GameplayEventStatBuffDetails statBuffEvent)
    {
        _shipInfo = shipDetails;
        _attachments = attachments;
        _shipTr = shipTr;
        _statBuffEvent = statBuffEvent;
    }

    private void Start()
    {
        SetupStats();
    }

    private void SetupStats()
    {
        _stats = new BuffableStat[4];

        _stats[0] = new BuffableStat(Info.Damage, Stat.Damage);
        _stats[1] = new BuffableStat(Info.Crit, Stat.Crit);
        _stats[2] = new BuffableStat(Info.Evade, Stat.Evade);
        _stats[3] = new BuffableStat(Info.Armor, Stat.Armor);
    }

    public void AddModifier(Stat stat, IStatModifier modifier)
    {
        _stats[(int)stat].AddModifier(modifier);

        int value = 0;
        modifier.ApplyModification(ref value);

        _statBuffEvent.TriggerEvent(gameObject.GetInstanceID(), new StatBuffDetails(value, stat, _stats[(int)stat].BuffedValue));
    }

    public void RemoveModifier(Stat stat, IStatModifier modifier)
    {
        _stats[(int)stat].RemoveModifier(modifier);
        _statBuffEvent.TriggerEvent(gameObject.GetInstanceID(), new StatBuffDetails(0, stat, _stats[(int)stat].BuffedValue));
    }

    public class Factory : PlaceholderFactory<ShipInfo, Vector3, Ship> { }

    private ShipInfo _shipInfo;
    private AttachmentHolderComponent _attachments;
    private Transform _shipTr;
}
