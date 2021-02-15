using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(TranslatableText))]
public class TranslatableTextEditor : UnityEditor.UI.TextEditor {
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("translatableText"));
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}