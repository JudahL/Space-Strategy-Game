using UnityEngine;

[CreateAssetMenu(fileName = "GameplayEventStatBuffDetails", menuName = "Events/GameplayEventStatBuffDetails")]
public class GameplayEventStatBuffDetails : GameplayEventGeneric<StatBuffDetails> { }

//PUT THIS IN ITS OWN FILE
public struct StatBuffDetails
{
    public int BuffAmount;
    public Stat StatType;
    public int TotalBuffedAmount;

    public StatBuffDetails(int buffAmount, Stat statType, int totalBuffedAmount)
    {
        BuffAmount = buffAmount;
        StatType = statType;
        TotalBuffedAmount = totalBuffedAmount;
    }
}