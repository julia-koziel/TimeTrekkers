using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(DemoProtocol))]
public class DemoProtocolEditor : Editor 
{
    SerializedProperty trialMatrix;
    SerializedProperty reactionTime;
    SerializedProperty trialNumbers;
    SerializedProperty wrongClicks;
    SerializedProperty preClickClips;
    SerializedProperty onClickClips;
    SerializedProperty postClickClips;
    ReorderableList trialProtocols;
    bool showTrialMatrix = false;
    int[] usedTrials 
    { 
        get => (new int[trialNumbers.arraySize])
                .ForEach((i, x) => trialNumbers.GetArrayElementAtIndex(i).intValue)
                .ToArray(); 
    }
    void OnEnable() 
    {
        trialMatrix = serializedObject.FindProperty("trialMatrix");
        reactionTime = serializedObject.FindProperty("reactionTime");
        trialNumbers = serializedObject.FindProperty("trialNumbers");
        wrongClicks = serializedObject.FindProperty("wrongClicks");
        preClickClips = serializedObject.FindProperty("preClickClips");
        onClickClips = serializedObject.FindProperty("onClickClips");
        postClickClips = serializedObject.FindProperty("postClickClips");


        trialProtocols = CreateList(serializedObject, serializedObject.FindProperty("trialNumbers"));

    }

    ReorderableList CreateList(SerializedObject obj, SerializedProperty prop)
    {
        ReorderableList list = new ReorderableList(obj, prop, true, true, true, true);

        list.drawElementCallback = (rect, index, active, focused) => {
            var trial = list.serializedProperty.GetArrayElementAtIndex(index);
            var click = wrongClicks.GetArrayElementAtIndex(index);
            var preClickClip = preClickClips.GetArrayElementAtIndex(index);
            var onClickClip = onClickClips.GetArrayElementAtIndex(index);
            var postClickClip = postClickClips.GetArrayElementAtIndex(index);

            rect.height = EditorGUIUtility.singleLineHeight;
            var rects = rect.DivideHorizontally(5);
            rects[0].width -= 25;
            rects[1].x -= 20;
            rects[1].width -= 20;
            rects[2].x -= 27;
            rects[2].width += 10;
            rects[3].x -= 18;
            rects[3].width += 10;
            rects[4].x -= 9;
            rects[4].width += 10;

            var tr = EditorGUI.IntField(rects[0], trial.intValue);
            if (tr != trial.intValue && tr.In(usedTrials))
            {
                EditorUtility.DisplayDialog("Error", "A protocol already exists for this trial", "OK");
            }
            else
            {
                trial.intValue = tr;
            }
            EditorGUI.PropertyField(rects[1], click, GUIContent.none);
            EditorGUI.PropertyField(rects[2], preClickClip, GUIContent.none);
            EditorGUI.PropertyField(rects[3], onClickClip, GUIContent.none);
            EditorGUI.PropertyField(rects[4], postClickClip, GUIContent.none);

        };

        list.drawHeaderCallback = (Rect rect) => {
            rect.width -= 10;
            rect.x += 10;
            var rects = rect.DivideHorizontally(5);
            rects[0].width -= 25;
            rects[1].x -= 20;
            rects[1].width -= 15;
            rects[2].x -= 27;
            rects[2].width += 10;
            rects[3].x -= 18;
            rects[3].width += 10;
            rects[4].x -= 9;
            rects[4].width += 10;

            EditorGUI.LabelField(rects[0], "Trial");
            EditorGUI.LabelField(rects[1], "Wrong");
            EditorGUI.LabelField(rects[2], "Pre Click");
            EditorGUI.LabelField(rects[3], "On Click");
            EditorGUI.LabelField(rects[4], "Post Click");
        };

        list.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            wrongClicks.arraySize++;
            preClickClips.arraySize++;
            onClickClips.arraySize++;
            postClickClips.arraySize++;
            l.index = index;
            var trial = l.serializedProperty.GetArrayElementAtIndex(index);
            trial.intValue = -1;
            wrongClicks.GetArrayElementAtIndex(index).boolValue = false;
            preClickClips.GetArrayElementAtIndex(index).objectReferenceValue = null;
            onClickClips.GetArrayElementAtIndex(index).objectReferenceValue = null;
            postClickClips.GetArrayElementAtIndex(index).objectReferenceValue = null;
            serializedObject.ApplyModifiedProperties();
        };

        list.onRemoveCallback = (ReorderableList l) => {
            var idx = l.index;
            l.index = -1;
            l.serializedProperty.DeleteArrayElementAtIndex(idx);
            wrongClicks.DeleteArrayElementAtIndex(idx);
            preClickClips.DeleteArrayElementAtIndex(idx);
            onClickClips.DeleteArrayElementAtIndex(idx);
            postClickClips.DeleteArrayElementAtIndex(idx);
            serializedObject.ApplyModifiedProperties();
        };

        list.onReorderCallbackWithDetails = (ReorderableList l, int oldIndex, int newIndex) => {
            wrongClicks.MoveArrayElement(oldIndex, newIndex);
            preClickClips.MoveArrayElement(oldIndex, newIndex);
            onClickClips.MoveArrayElement(oldIndex, newIndex);
            postClickClips.MoveArrayElement(oldIndex, newIndex);
        };

        return list;
    }
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(trialMatrix);

        if (trialMatrix.objectReferenceValue != null)
        {
            showTrialMatrix = EditorGUILayout.BeginFoldoutHeaderGroup(showTrialMatrix, "Edit Trial Matrix");
            if (showTrialMatrix)
            {
                var editor = Editor.CreateEditor(trialMatrix.objectReferenceValue, typeof(TrialMatrixEditor));
                editor.OnInspectorGUI();
            }
        }

        EditorGUILayout.PropertyField(reactionTime);

        EditorGUILayout.Space();
        trialProtocols.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}