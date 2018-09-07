using UnityEngine;
using Zenject;

public class StatBuffStatusIcons : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _statBuffSprites;

    private StatusIconManager _iconManager;

    [Inject]
    public void Construct(StatusIconManager iconManager)
    {
        _iconManager = iconManager;
    }

    public void OnStatBuff(StatBuffDetails details)
    {
        if (details.BuffAmount != 0)
        {
            _iconManager.AddIcon(_statBuffSprites[(int)details.StatType]);
        } 
        else
        {
            _iconManager.RemoveIcon(_statBuffSprites[(int)details.StatType]);
        }
    }
}
