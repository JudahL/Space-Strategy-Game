using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int currentTeamIndex = 0;

    [SerializeField]
    private int[] _teamIds;

    [SerializeField]
    private GameplayEventStandard _newTurnEvent;
    [SerializeField]
    private GameplayEventStandard _endTurnEvent;

    private void Start()
    {
        StartNextTurn();        
    }

    private void StartNextTurn()
    {
        _newTurnEvent.TriggerEvent(_teamIds[currentTeamIndex]);
    }

    public void EndTurn()
    {
        _endTurnEvent.TriggerEvent(_teamIds[currentTeamIndex]);
        IncrementTeamIndex();
        StartNextTurn();
    }

    private void IncrementTeamIndex()
    {
        currentTeamIndex++;
        currentTeamIndex %= _teamIds.Length;
    }
}

