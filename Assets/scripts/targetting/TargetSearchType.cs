using UnityEngine;

[CreateAssetMenu(fileName = "TargetSearchType", menuName = "Setup/TargetSearchType")]
public class TargetSearchType : ScriptableObject
{
    [SerializeField]
    private TargettingTagManager _tagManager;
    [SerializeField]
    private TargetType[] _viableTargets;

    public LayerMask GetLayerMask(int teamIndex)
    {
        return LayerMask.GetMask(_tagManager.GetTags(teamIndex, _viableTargets));
    }	
}
