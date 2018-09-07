using UnityEngine;

public enum TargetType { Ally, Enemy }

[System.Serializable]
public struct TeamSearchTags
{
    [SerializeField]
    private string _teamName;
    [SerializeField][LayerSelector]
    private string[] _targetTags;

    public string GetTag(TargetType targetType)
    {
        return _targetTags[(int)targetType];
    }

    public string[] GetTags(TargetType[] targetTypes)
    {
        string[] tags = new string[targetTypes.Length];

        for (int i = 0; i < targetTypes.Length; i++)
        {
            tags[i] = _targetTags[(int)targetTypes[i]];
        }

        return tags;
    }
}
[CreateAssetMenu(fileName = "TargettingTagManager", menuName = "Setup/TargettingManager")]
public class TargettingTagManager : ScriptableObject
{
    [SerializeField]
    private TeamSearchTags[] _teamTypeTags;

    public TeamSearchTags GetTargettingTags(int teamIndex)
    {
        return _teamTypeTags[teamIndex];
    }

    public string[] GetTags(int teamIndex, TargetType[] targetTypes)
    {
        teamIndex %= 10;
        return _teamTypeTags[teamIndex].GetTags(targetTypes);
    }
}
