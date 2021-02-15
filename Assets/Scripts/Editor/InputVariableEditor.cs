using UnityEngine;
using MathNet.Numerics;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
// using VT = InputVariable.VariableType;
using System;

[CustomEditor(typeof(InputVariable))]
[CanEditMultipleObjects]
public class InputVariableEditor : Editor
{
    SerializedProperty type;
    SerializedProperty weighted;
    SerializedProperty weights;
    SerializedProperty controlSpacing;
    SerializedProperty minSpacing;
    ReorderableList weightedVars;
    ReorderableList vars;
    SerializedProperty useGenerator;
    ReorderableList weighedVals;
    ReorderableList vals;
    SerializedProperty start;
    SerializedProperty stop;
    SerializedProperty step;

    void OnEnable() 
    {
        type = serializedObject.FindProperty("type");
        weighted = serializedObject.FindProperty("weighted");
        weights = serializedObject.FindProperty("weights");
        controlSpacing = serializedObject.FindProperty("controlSpacing");
        minSpacing = serializedObject.FindProperty("minSpacing");

        weightedVars = new ReorderableList(serializedObject, serializedObject.FindProperty("variables"), true, true, true, true);
        vars = new ReorderableList(serializedObject, serializedObject.FindProperty("variables"), true, true, true, true);
        useGenerator = serializedObject.FindProperty("useGenerator");
        weighedVals = new ReorderableList(serializedObject, serializedObject.FindProperty("weightedValues"), true, true, true, true);
        vals = new ReorderableList(serializedObject, serializedObject.FindProperty("values"), true, true, true, true);
        start = serializedObject.FindProperty("start");
        stop = serializedObject.FindProperty("stop");
        step = serializedObject.FindProperty("step");

        weightedVars.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = weightedVars.serializedProperty.GetArrayElementAtIndex(index);
            var weight = weights.GetArrayElementAtIndex(index);
            var variableRect = new Rect(rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight);
            var weightRect = new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(variableRect, element, GUIContent.none);
            EditorGUI.PropertyField(weightRect, weight, GUIContent.none);
        };

        vars.drawElementCallback  = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = vars.serializedProperty.GetArrayElementAtIndex(index);
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };

        weightedVars.drawHeaderCallback = (Rect rect) => {
            rect.height = EditorGUIUtility.singleLineHeight;
            var leftRect = new Rect(rect.x, rect.y, rect.width - 45, rect.height);
            var rightRect = new Rect(rect.x + rect.width - 45, rect.y, 45, rect.height);
            EditorGUI.LabelField(leftRect, "Variables");
            EditorGUI.LabelField(rightRect, "Weights");
        };

        vars.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Variables");
        };

        weightedVars.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            weights.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            var weight = weights.GetArrayElementAtIndex(index);
            
            element.objectReferenceValue = null;
            weight.floatValue = 1;
            serializedObject.ApplyModifiedProperties();
        };

        vars.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            weights.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            var weight = weights.GetArrayElementAtIndex(index);
            element.objectReferenceValue = null;
            weight.floatValue = 1;
            serializedObject.ApplyModifiedProperties();
        };

        weightedVars.onRemoveCallback = (ReorderableList l) => {
            ReorderableList.defaultBehaviours.DoRemoveButton(l);
            weights.arraySize--;
            serializedObject.ApplyModifiedProperties();
        };

        vars.onRemoveCallback = (ReorderableList l) => {
            ReorderableList.defaultBehaviours.DoRemoveButton(l);
            weights.arraySize--;
            serializedObject.ApplyModifiedProperties();
        };

        weightedVars.onReorderCallbackWithDetails = (ReorderableList l, int oldIndex, int newIndex) => {
            l.serializedProperty.MoveArrayElement(oldIndex, newIndex);
            weights.MoveArrayElement(oldIndex, newIndex);
        };

        vars.onReorderCallbackWithDetails = (ReorderableList l, int oldIndex, int newIndex) => {
            l.serializedProperty.MoveArrayElement(oldIndex, newIndex);
            weights.MoveArrayElement(oldIndex, newIndex);
        };

        weighedVals.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = weighedVals.serializedProperty.GetArrayElementAtIndex(index);
            var variableRect = new Rect(rect.x, rect.y, rect.width - 40, EditorGUIUtility.singleLineHeight);
            var weightRect = new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(variableRect, element.FindPropertyRelative("value"), GUIContent.none);
            EditorGUI.PropertyField(weightRect, element.FindPropertyRelative("weight"), GUIContent.none);
        };

        weighedVals.drawHeaderCallback = (Rect rect) => {
            var leftRect = new Rect(rect.x, rect.y, rect.width - 45, rect.height);
            var rightRect = new Rect(rect.x + rect.width - 45, rect.y, 45, rect.height);
            EditorGUI.LabelField(leftRect, "Values");
            EditorGUI.LabelField(rightRect, "Weights");
        };

        

        vals.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = vals.serializedProperty.GetArrayElementAtIndex(index);
            var newRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.FloatField(newRect, element.floatValue);
        };

        vals.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Values");
        };
    }

   

    // EditorGUILayout.PropertyField(type);
        // EditorGUILayout.Space();
        // // EditorGUILayout.PropertyField(weighted, new GUIContent("Weighted"));
        // weighted.boolValue = EditorGUILayout.ToggleLeft("Weighted", weighted.boolValue);
        // EditorGUILayout.Space();
        // serializedObject.ApplyModifiedProperties();
        
        // switch ((VT)type.enumValueIndex)
        // {
        //     case VT.Categorical:
        //         if (weighted.boolValue)
        //         {
        //             weightedCategoricalValues.DoLayoutList();
        //         }
        //         else
        //         {
        //             categoricalValues.DoLayoutList();
        //         }

        //         break;
            
        //     case VT.Discrete:
        //         useGenerator.boolValue = EditorGUILayout.ToggleLeft("Use Generator", useGenerator.boolValue);
        //         EditorGUILayout.Space();
        //         if (useGenerator.boolValue)
        //         {
        //             EditorGUILayout.PropertyField(start);
        //             EditorGUILayout.PropertyField(stop);
        //             EditorGUILayout.PropertyField(step);

        //             var test = Generate.LinearRange(start.floatValue, step.floatValue, stop.floatValue);
                    
        //             var example = test.Length <= 10
        //                         ? ", ".Join(test)
        //                         : ", ".Join(test.Take(5)) + ", ..., " + ", ".Join(test.Skip(test.Length-5));

        //             EditorGUILayout.Space();
        //             EditorGUILayout.LabelField(example);
        //         }
        //         else if (weighted.boolValue)
        //         {
        //             weightedValues.DoLayoutList();
        //         }
        //         else
        //         {
        //             values.DoLayoutList();
        //         }
        //         break;

        //     case VT.Continuous:
        //         EditorGUILayout.PropertyField(start);
        //         EditorGUILayout.PropertyField(stop);
        //         break;
        // }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.Space();
        // EditorGUILayout.PropertyField(weighted, new GUIContent("Weighted"));
        weighted.boolValue = EditorGUILayout.ToggleLeft("Weighted", weighted.boolValue);
        EditorGUILayout.Space();
        serializedObject.ApplyModifiedProperties();
        
        // switch ((VT)type.enumValueIndex)
        // {
        //     case VT.Categorical:
        //         if (weighted.boolValue)
        //         {
        //             weightedVars.DoLayoutList();
        //         }
        //         else
        //         {
        //             vars.DoLayoutList();
        //         }

        //         break;
            
        //     case VT.Discrete:
        //         useGenerator.boolValue = EditorGUILayout.ToggleLeft("Use Generator", useGenerator.boolValue);
        //         EditorGUILayout.Space();
        //         if (useGenerator.boolValue)
        //         {
        //             EditorGUILayout.PropertyField(start);
        //             EditorGUILayout.PropertyField(stop);
        //             EditorGUILayout.PropertyField(step);

        //             var test = Generate.LinearRange(start.floatValue, step.floatValue, stop.floatValue);
                    
        //             var example = test.Length <= 10
        //                         ? ", ".Join(test)
        //                         : ", ".Join(test.Take(5)) + ", ..., " + ", ".Join(test.Skip(test.Length-5));

        //             EditorGUILayout.Space();
        //             EditorGUILayout.LabelField(example);
        //         }
        //         else if (weighted.boolValue)
        //         {
        //             weighedVals.DoLayoutList();
        //         }
        //         else
        //         {
        //             vals.DoLayoutList();
        //         }
        //         break;

        //     case VT.Continuous:
        //         EditorGUILayout.PropertyField(start);
        //         EditorGUILayout.PropertyField(stop);
        //         break;
        // }

        serializedObject.ApplyModifiedProperties();

    }
}