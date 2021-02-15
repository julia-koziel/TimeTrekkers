using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(TranslatableAudioClip))]
public class TranslatableAudioClipEditor : Editor 
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
    SerializedProperty translatedAudioClips;
    SerializedProperty isMuteable;
    void OnEnable() 
    {
        serializedObject.Update();

        languageManager = serializedObject.FindProperty("languageManager");
        translatedAudioClips = serializedObject.FindProperty("translatedAudioClips");
        isMuteable = serializedObject.FindProperty("isMuteable");

        if (languageManager.objectReferenceValue == null)
        {
            var guids = AssetDatabase.FindAssets("t:LanguageManager");
            if (guids.Length > 0) 
            {
                if (guids.Length > 1) Debug.LogWarning("More than one language manager found. There should only be one.");

                var lm = (LanguageManager)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(LanguageManager));
                languageManager.objectReferenceValue = lm;
                translatedAudioClips.arraySize = languageCodes.Length;
            } 
            else
            {
                Debug.LogWarning("No language manager found");
                translatedAudioClips.arraySize = 0;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(languageManager);
        EditorGUILayout.Space();

        for (int i = 0; i < translatedAudioClips.arraySize; i++)
        {
            // var rect = EditorGUILayout.BeginHorizontal();
            // var rect1 = new Rect(rect.x, rect.y, 30, rect.height);
            // var rect2 = new Rect(rect.x + 30, rect.y, rect.width - 30, rect.height);
            EditorGUILayout.LabelField(languageCodes[i]);
            EditorGUILayout.PropertyField(translatedAudioClips.GetArrayElementAtIndex(i), GUIContent.none);
            // EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.PropertyField(isMuteable);

        serializedObject.ApplyModifiedProperties();
    }
}