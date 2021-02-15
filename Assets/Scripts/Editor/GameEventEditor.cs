using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameEvent myEvent = (GameEvent) target;
        if (GUILayout.Button("Raise"))
        {
            myEvent.Raise();
        }

    }
}