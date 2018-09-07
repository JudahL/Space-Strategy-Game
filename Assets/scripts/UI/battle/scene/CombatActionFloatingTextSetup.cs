using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatActionFloatingTextSetup", menuName = "FloatingText/CombatActionSetup")]
public class CombatActionFloatingTextSetup : ScriptableObject
{
    [System.Serializable]
    private struct TypeInfo
    {
        public CombatActionType Type;
        public AnimatedTextInfo Info;
    }

    [SerializeField]
    private TypeInfo[] _typeSetups;

    public Dictionary<CombatActionType, AnimatedTextInfo> GetSetupDictionary()
    {
        Dictionary<CombatActionType, AnimatedTextInfo> dictionary = new Dictionary<CombatActionType, AnimatedTextInfo>();

        for (int i = 0; i < _typeSetups.Length; i++)
        {
            dictionary.Add(_typeSetups[i].Type, _typeSetups[i].Info);
        }

        return dictionary;
    }
}
