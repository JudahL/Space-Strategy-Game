using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameplayEventStandard))]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameplayEventStandard e = target as GameplayEventStandard;
        if (GUILayout.Button("Trigger Event"))
            e.TriggerEvent(1);
    }
}

[CustomEditor(typeof(GameplayEventInt))]
public class EventGenericEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameplayEventInt e = target as GameplayEventInt;
        if (GUILayout.Button("Trigger Event"))
            e.TriggerEvent(1, 0);
    }
}

