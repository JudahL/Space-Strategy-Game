using UnityEngine;

public interface ITargetSystemClient
{
    bool TryAddTarget(GameObject potentialTarget, out bool allTargetsAcquired);	
}
