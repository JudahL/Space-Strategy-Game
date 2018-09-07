using UnityEngine;
using Zenject;

public class SelectionComponent : MonoBehaviour, ISelectionComponent
{
    private GameplayEventGameObject _objectSelectedEvent;
    private int _teamId;

    [Inject]
    public void Construct([Inject(Id = 2)]GameplayEventGameObject objectSelectedEvent, int teamId)
    {
        _objectSelectedEvent = objectSelectedEvent;
        _teamId = teamId;
    }

    public void Select()
    {
        _objectSelectedEvent.TriggerEvent(_teamId, gameObject);
    }
}
