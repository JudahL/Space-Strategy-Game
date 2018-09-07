using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class TargetSelectionArrows : MonoBehaviour
{   
    private TargetSelectionArrow.Pool _arrowPool;
    private Dictionary<GameObject, List<TargetSelectionArrow>> _targets = new Dictionary<GameObject, List<TargetSelectionArrow>>();

    [Inject]
    public void Construct(TargetSelectionArrow.Pool pool)
    {
        _arrowPool = pool;
    }

    public void AddTarget(GameObject target)
    {
        Transform targetTransform = target.transform;
        
        TargetSelectionArrow arrow = _arrowPool.Spawn(targetTransform);

        if (!_targets.ContainsKey(target))
        {
            AddNewTarget(target);
        }

        _targets[target].Add(arrow);
    }

    public void RemoveTarget(GameObject target)
    {
        _arrowPool.Despawn(_targets[target][_targets[target].Count - 1]);
        _targets[target].RemoveAt(_targets[target].Count-1);
    }

    private void AddNewTarget(GameObject target)
    {
        _targets.Add(target, new List<TargetSelectionArrow>());
    }
    
    public void ClearTargets()
    {
        foreach (KeyValuePair<GameObject, List<TargetSelectionArrow>> entry in _targets)
        {
            for (int i = 0; i < entry.Value.Count; i++)
            {
                _arrowPool.Despawn(entry.Value[i]);
            }

            entry.Value.Clear();
        }
    }	
}