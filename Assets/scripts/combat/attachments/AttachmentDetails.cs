public struct AttachmentDetails
{
    public string Name;
    public string Description;
    public int Cooldown;
    public int MaximumTargets;

    public AttachmentDetails (string name, string desc, int cooldown, int maxTargets)
    {
        Name = name;
        Description = desc;
        Cooldown = cooldown;
        MaximumTargets = maxTargets;
    }	
}
