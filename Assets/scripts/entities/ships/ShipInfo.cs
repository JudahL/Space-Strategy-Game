using UnityEngine;

[System.Serializable]
public class ShipInfo 
{
    [SerializeField]
    private ShipSettings _type;
    [SerializeField]
    private int _experience;
    [SerializeField]
    private int _level;
    [SerializeField]
    private AttachmentSettings[] _attachments;
    
    //XP + LEVEL
    public int Experience   { get { return _experience; }   private set { _experience = value; } }
    public int Level        { get { return _level; }        private set { _level = value; } }

    //NAME + ART
    public string Name      { get { return _type.ShipName; } }
    public Sprite Artwork   { get { return _type.Image; } }

    //STATS
    public int Health   { get { return _type.GetHealth(_level); } }
    public int Damage   { get { return _type.GetDamage(_level); } }
    public int Crit     { get { return _type.GetCrit(_level); } }
    public int Evade    { get { return _type.GetEvade(_level); } }
    public int Armor    { get { return _type.GetArmor(_level); } }

    //public int Health   { get { return _health; } }
    //public int Damage   { get { return _damage; } }
    //public int Crit     { get { return _crit; } }
    //public int Evade    { get { return _evade; } }
    //public int Armor    { get { return _armor; } }

    //ATTACHMENTS
    public AttachmentSettings[] Attachments { get { return _attachments; } private set { _attachments = value; } }

    //TEAM
    public int TeamId { get; private set; }

    public ShipInfo(ShipSettings type, int xp, int level, AttachmentSettings[] attachments, int teamId)
    {
        _type = type;
        Experience = xp;
        Level = level;
        Attachments = attachments;
        TeamId = teamId;

        //_health = _type.GetHealth(Level);
        //_damage = _type.GetDamage(Level);
        //_crit = _type.GetCrit(Level);
        //_evade = _type.GetEvade(Level);
        //_armor = _type.GetArmor(Level);
    }

    public void AddExperience(int amount)
    {
        Experience += amount;
    }

    //private int _health;
    //private int _damage;
    //private int _crit;
    //private int _evade;
    //private int _armor;
}
