public struct Cooldown
{
    public int Remaining;
    public int Total;

    public float Ratio
    {
        get
        {
            if (Total > 0f)
            {
                return Remaining * 1f / Total * 1f;
            } 
            else
            {
                return 0f;
            }
        }
    }
}
