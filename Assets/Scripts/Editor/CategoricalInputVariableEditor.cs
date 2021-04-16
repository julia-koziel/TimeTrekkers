using UnityEngine;
using MathNet.Numerics;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
// using VT = InputVariable.VariableType;
using System;

[CustomEditor(typeof(CategoricalInputVariable))]
[CanEditMultipleObjects]
public class CategoricalInputVariableEditor : Editor
{
    SerializedProperty weighted;
    SerializedProperty weights;
    SerializedProperty controlSpacing;
    SerializedProperty minSpacing;
    ReorderableList weightedVars;

    void OnEnable() 
    {
        weighted = serializedObject.FindProperty("weighted");
        weights = serializedObject.FindProperty("weights");
        controlSpacing = serializedObject.FindProperty("controlSpacing");
        minSpacing = serializedObject.FindProperty("minSpacing");

        minSpacing.arraySize = weights.arraySize;
        serializedObject.ApplyModifiedProperties();

        weightedVars = new ReorderableList(serializedObject, serializedObject.FindProperty("variables"), true, true, true, true);
        List<float> heights = new List<float>(weightedVars.serializedProperty.arraySize);

        weightedVars.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            float heightUnit = EditorGUIUtility.singleLineHeight * 1.25f;
            float heightMargin = EditorGUIUtility.singleLineHeight * 0.125f;
            float height = heightUnit;
            rect.y += heightMargin;
            rect.height = EditorGUIUtility.singleLineHeight;

            var element = weightedVars.serializedProperty.GetArrayElementAtIndex(index);
            var weight = weights.GetArrayElementAtIndex(index);
            var rectLeft = new Rect(rect.x, rect.y, rect.width - 45, rect.height);
            var rectRight = new Rect(rect.x + rect.width - 40, rect.y, 40, rect.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            if (weighted.boolValue)
            {
                if (controlSpacing.boolValue)
                {
                    rectLeft.width -= 55;
                    var rectMiddle = new Rect(rect.x + rect.width - 95, rect.y, 40, rect.height);

                    EditorGUI.PropertyField(rectMiddle, minSpacing.GetArrayElementAtIndex(index), GUIContent.none);
                }

                EditorGUI.PropertyField(rectLeft, element, GUIContent.none);
                EditorGUI.PropertyField(rectRight, weight, GUIContent.none);

            }
            else EditorGUI.PropertyField(rect, element, GUIContent.none);

        };

        weightedVars.drawHeaderCallback = (Rect rect) => {
            rect.height = EditorGUIUtility.singleLineHeight;
            var leftRect = new Rect(rect.x, rect.y, rect.width - 45, rect.height);
            var rightRect = new Rect(rect.x + rect.width - 45, rect.y, 45, rect.height);
            if (weighted.boolValue)
            {
                if (controlSpacing.boolValue)
                {
                    leftRect.width -= 55;
                    var middleRect = new Rect(rect.x + rect.width - 100, rect.y, 45, rect.height);
                    EditorGUI.LabelField(middleRect, "Spacing");
                }
                EditorGUI.LabelField(leftRect, "Variable");
                EditorGUI.LabelField(rightRect, "Weight");
            }
            else EditorGUI.LabelField(rect, "Variables");
            
        };

        weightedVars.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            weights.arraySize++;
            minSpacing.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            var weight = weights.GetArrayElementAtIndex(index);
            
            element.objectReferenceValue = null;
            weight.intValue = 1;
            serializedObject.ApplyModifiedProperties();
        };

        weightedVars.onRemoveCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize - 2;
            l.serializedProperty.arraySize--;
            weights.arraySize--;
            minSpacing.arraySize--;
            l.index = index;
            serializedObject.ApplyModifiedProperties();
        };

        weightedVars.onReorderCallbackWithDetails = (ReorderableList l, int oldIndex, int newIndex) => {
            weights.MoveArrayElement(oldIndex, newIndex);
            minSpacing.MoveArrayElement(oldIndex, newIndex);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        
        weighted.boolValue = EditorGUILayout.ToggleLeft("Weighted", weighted.boolValue);
        if (weighted.boolValue) controlSpacing.boolValue = EditorGUILayout.ToggleLeft("Control Spacing", controlSpacing.boolValue);
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        weightedVars.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}