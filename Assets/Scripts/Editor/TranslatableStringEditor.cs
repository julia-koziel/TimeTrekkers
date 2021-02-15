using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(TranslatableString))]
public class TranslatableStringEditor : Editor 
{
    string[] languageCodes
    {
        get
        {
            var codes = (languageManager.objectReferenceValue as LanguageManager)?.languageCodes;
            return codes?.Select(sv => sv.name).ToArray() ?? new string[] {};
        }
    }
    SerializedProperty languageManager;
    SerializedProperty translatedStrings;
    void OnEnable() 
    {
        serializedObject.Update();

        languageManager = serializedObject.FindProperty("languageManager");
        translatedStrings = serializedObject.FindProperty("translatedStrings");

        if (languageManager.objectReferenceValue == null)
        {
            var guids = AssetDatabase.FindAssets("t:LanguageManager");
            if (guids.Length > 0) 
            {
                if (guids.Length > 1) Debug.LogWarning("More than one language manager found. There should only be one.");

                var lm = (LanguageManager)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(LanguageManager));
                languageManager.objectReferenceValue = lm;
                translatedStrings.arraySize = languageCodes.Length;
            } 
            else
            {
                Debug.LogWarning("No language manager found");
                translatedStrings.arraySize = 0;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(languageManager);
        EditorGUILayout.Space();

        for (int i = 0; i < translatedStrings.arraySize; i++)
        {
            // var rect = EditorGUILayout.BeginHorizontal();
            // var rect1 = new Rect(rect.x, rect.y, 30, rect.height);
            // var rect2 = new Rect(rect.x + 30, rect.y, rect.width - 30, rect.height);
            var text = translatedStrings.GetArrayElementAtIndex(i);
            EditorGUILayout.LabelField(languageCodes[i]);
            text.stringValue = EditorGUILayout.TextArea(text.stringValue);
            // EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}