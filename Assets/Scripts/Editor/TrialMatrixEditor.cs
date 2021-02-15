using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(TrialMatrix))]
public class TrialMatrixEditor : Editor 
{
    SerializedProperty ivman;
    SerializedProperty shuffle;
    int nVariables;
    string[] variableNames;
    ReorderableList list;
    bool doneSetup = false;
    
    void OnEnable() 
    {
        ivman = serializedObject.FindProperty("inputVariablesManager");
        shuffle = serializedObject.FindProperty("shuffle");

        if (ivman.objectReferenceValue != null)
        {
            DoSetup();
        }
    }

    void DoSetup()
    {
        var ivs = (ivman.objectReferenceValue as InputVariablesManager).inputVariables;
        nVariables = ivs.Length;
        variableNames = ivs.Select(iv => iv.name).ToArray();


        list = new ReorderableList(serializedObject, serializedObject.FindProperty("rowArray"), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) => {
            rect.x += 35;
            rect.width -= 35;
            var rects = rect.DivideHorizontally(nVariables);
            variableNames.ForEach((i, vn) => EditorGUI.LabelField(rects[i], vn));
        };

        list.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            
            element.FindPropertyRelative("row").arraySize = nVariables;
            serializedObject.ApplyModifiedProperties();
        };

        list.drawElementCallback = (rect, index, active, focused) => {
            rect.height = EditorGUIUtility.singleLineHeight;
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            var row = element.FindPropertyRelative("row");
            var trialRect = new Rect(rect.x, rect.y, 20, rect.height);
            EditorGUI.LabelField(trialRect, index.ToString());
            rect.x += 25;
            rect.width -= 25;
            var rects = rect.DivideHorizontally(row.arraySize);
            rects.ForEach((i, r) => rects[i].width -= 10);
            rects.ForEach((i, r) => EditorGUI.PropertyField(r, row.GetArrayElementAtIndex(i), GUIContent.none));
        };

        doneSetup = true;
    }

    
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(ivman, new GUIContent("IVManager"));

        EditorGUILayout.PropertyField(shuffle);

        if (ivman.objectReferenceValue != null)
        {
            if (!doneSetup) DoSetup();
            else list.DoLayoutList();
        }
        
        serializedObject.ApplyModifiedProperties();
        
    }
}