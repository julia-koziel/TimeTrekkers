using UnityEngine;
using UnityEditor;
using System.Linq;
using MathNet.Numerics;

[CustomEditor(typeof(DiscreteInputVariable))]

public class DiscreteInputVariableEditor : Editor 
{
    SerializedProperty useGenerator;
    SerializedProperty start;
    SerializedProperty step;
    SerializedProperty stop;

    void OnEnable()
    {
        useGenerator = serializedObject.FindProperty("useGenerator");
        start = serializedObject.FindProperty("start");
        step = serializedObject.FindProperty("step");
        stop = serializedObject.FindProperty("stop");
    }
    public override void OnInspectorGUI() 
    {
        serializedObject.Update();
        
        useGenerator.boolValue = EditorGUILayout.ToggleLeft("Use Generator", useGenerator.boolValue);
        if (useGenerator.boolValue)
        {
            EditorGUILayout.PropertyField(start);
            EditorGUILayout.PropertyField(step);
            EditorGUILayout.PropertyField(stop);
            EditorGUILayout.Space();

            var sample = Generate.LinearRange(start.floatValue, step.floatValue, stop.floatValue);
            string sampleString;
            if (sample.Length < 5) sampleString = ", ".Join(sample);
            else
            {
                sampleString = $"{sample[0]}, {sample[1]}, {sample[2]}, ..., {sample.Last()}";
            }

            EditorGUILayout.LabelField(sampleString);
        }
        else
        {
            EditorGUILayout.LabelField("Not implemented yet");
        }

        serializedObject.ApplyModifiedProperties();
    }
}