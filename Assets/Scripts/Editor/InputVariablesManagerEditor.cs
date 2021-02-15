using UnityEngine;
using MathNet.Numerics;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
// using VT = InputVariable.VariableType;
using System;

[CustomEditor(typeof(InputVariablesManager))]
[CanEditMultipleObjects]
public class InputVariablesManagerEditor : Editor
{
    ReorderableList inputVariables;
    SerializedProperty trial;
    SerializedProperty nTrials;
    InputVariable[] existingIVs;

    void OnEnable() 
    {
        string[] guids;

        // search for a ScriptObject called ScriptObj
        guids = AssetDatabase.FindAssets("t:InputVariable");
        existingIVs = guids.Select(g => (InputVariable)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(g), typeof(InputVariable))).ToArray();

        trial = serializedObject.FindProperty("trial");
        nTrials = serializedObject.FindProperty("nTrials");

        inputVariables = CreateList(serializedObject, serializedObject.FindProperty("inputVariables"));


        

        // type = serializedObject.FindProperty("type");
        // weighted = serializedObject.FindProperty("weighted");
        // weightedCategoricalValues = new ReorderableList(serializedObject, serializedObject.FindProperty("weightedCategoricalValues"), true, true, true, true);
        // categoricalValues = new ReorderableList(serializedObject, serializedObject.FindProperty("categoricalValues"), true, true, true, true);
        // useGenerator = serializedObject.FindProperty("useGenerator");
        // weightedValues = new ReorderableList(serializedObject, serializedObject.FindProperty("weightedValues"), true, true, true, true);
        // values = new ReorderableList(serializedObject, serializedObject.FindProperty("values"), true, true, true, true);
        // start = serializedObject.FindProperty("start");
        // stop = serializedObject.FindProperty("stop");
        // step = serializedObject.FindProperty("step");

        // weightedCategoricalValues.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
        //     var element = weightedCategoricalValues.serializedProperty.GetArrayElementAtIndex(index);
        //     var variableRect = new Rect(rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight);
        //     var weightRect = new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight);

        //     // Draw fields - passs GUIContent.none to each so they are drawn without labels
        //     EditorGUI.PropertyField(variableRect, element.FindPropertyRelative("variable"), GUIContent.none);
        //     EditorGUI.PropertyField(weightRect, element.FindPropertyRelative("weight"), GUIContent.none);
        // };

        // weightedCategoricalValues.drawHeaderCallback = (Rect rect) => {
        //     var leftRect = new Rect(rect.x, rect.y, rect.width - 45, rect.height);
        //     var rightRect = new Rect(rect.x + rect.width - 45, rect.y, 45, rect.height);
        //     EditorGUI.LabelField(leftRect, "Variables");
        //     EditorGUI.LabelField(rightRect, "Weights");
        // };

        // weightedValues.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
        //     var element = weightedValues.serializedProperty.GetArrayElementAtIndex(index);
        //     var variableRect = new Rect(rect.x, rect.y, rect.width - 40, EditorGUIUtility.singleLineHeight);
        //     var weightRect = new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight);

        //     // Draw fields - passs GUIContent.none to each so they are drawn without labels
        //     EditorGUI.PropertyField(variableRect, element.FindPropertyRelative("value"), GUIContent.none);
        //     EditorGUI.PropertyField(weightRect, element.FindPropertyRelative("weight"), GUIContent.none);
        // };

        // weightedValues.drawHeaderCallback = (Rect rect) => {
        //     var leftRect = new Rect(rect.x, rect.y, rect.width - 45, rect.height);
        //     var rightRect = new Rect(rect.x + rect.width - 45, rect.y, 45, rect.height);
        //     EditorGUI.LabelField(leftRect, "Values");
        //     EditorGUI.LabelField(rightRect, "Weights");
        // };

        // categoricalValues.drawHeaderCallback = (Rect rect) => {
        //     EditorGUI.LabelField(rect, "Variables");
        // };

        // values.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
        //     var element = values.serializedProperty.GetArrayElementAtIndex(index);
        //     var newRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

        //     // Draw fields - passs GUIContent.none to each so they are drawn without labels
        //     EditorGUI.FloatField(newRect, element.floatValue);
        // };

        // values.drawHeaderCallback = (Rect rect) => {
        //     EditorGUI.LabelField(rect, "Values");
        // };
    }

    ReorderableList CreateList (SerializedObject obj, SerializedProperty prop)
    {
        ReorderableList list = new ReorderableList (obj, prop, true, true, true, true);

        List<float> heights = new List<float> (prop.arraySize);

        list.drawElementCallback = (rect, index, active, focused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex (index);
            
            float heightUnit = EditorGUIUtility.singleLineHeight * 1.5f;
            float heightMargin = EditorGUIUtility.singleLineHeight * 0.25f;
            float height = heightUnit;
            rect.y += heightMargin;
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(rect, element, GUIContent.none);

            var name = element.objectReferenceValue?.name ?? "";
            if (name == "")
            {
                // var placeholder = "Create with name: ";
                // var subrect = EditorGUI.PrefixLabel(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), new GUIContent(placeholder));
                // name = EditorGUI.DelayedTextField(subrect, name);
                // if (name != "")
                // {
                //     var asset = ScriptableObject.CreateInstance<InputVariable>();

                //     AssetDatabase.CreateAsset(asset, $"Assets/Variables/{name}.asset");
                //     AssetDatabase.SaveAssets();
                //     EditorUtility.FocusProjectWindow();
                //     element.objectReferenceValue = asset;
                //     EditorGUIUtility.PingObject(asset);
                // }
            }
            else
            {   
                
                var elObj = element.objectReferenceValue;

                if (elObj is CategoricalInputVariable)
                {
                    var catIV = (CategoricalInputVariable)elObj;
                    var weights = catIV.weights;
                    var vars = catIV.variables;

                    var summary = vars.Select(v => v.name);
                    
                    if (catIV.weighted)
                    {
                        summary = summary.Zip(weights.Select(w => w.ToString()), (v, w) => $"{v} ({w})");
                    }

                    height += heightUnit;
                    rect.y += heightUnit;
                    EditorGUI.LabelField(rect, ", ".Join(summary));
                }

                if (active)
                {
                    height += heightUnit;
                    rect.y += heightUnit;
                    rect.x += rect.width - 30;
                    rect.width = 30;
                    
                    if (GUI.Button(rect, "Edit"))
                    {
                        Selection.SetActiveObjectWithContext(elObj, null);
                    }
                }
            }

            try {
                heights[index] = height;
            } catch (ArgumentOutOfRangeException e) {
                Debug.LogWarning (e.Message);
            } finally {
                float[] floats = heights.ToArray ();
                Array.Resize(ref floats, prop.arraySize);
                heights = floats.ToList ();
            }
        };

        list.elementHeightCallback = (index) => {
            Repaint ();
            float height = 0;

            try {
                height = heights[index];
            } catch (ArgumentOutOfRangeException e) {
                Debug.LogWarning (e.Message);
            } finally {
                float[] floats = heights.ToArray ();
                Array.Resize (ref floats, prop.arraySize);
                heights = floats.ToList ();
            }

            return height;
        };

        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Input Variables");
        };

        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();

            // TODO add different types of IVs
            menu.AddItem(new GUIContent("Create New"), false, () => { ReorderableList.defaultBehaviours.DoAddButton(list); });
            
            foreach (var iv in existingIVs) {
                menu.AddItem(new GUIContent(iv.name), false, clickHandler, iv);
            }
            
            menu.ShowAsContext();
        };

        list.onRemoveCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize - 2;
            l.serializedProperty.arraySize--;
            l.index = index;
            serializedObject.ApplyModifiedProperties();
        };

        return list;
    }

    

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(nTrials);
        EditorGUILayout.PropertyField(trial);
        EditorGUILayout.Space();

        inputVariables.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

    }

    private void clickHandler(object target) 
    {
        var iv = (InputVariable)target;
        var index = inputVariables.serializedProperty.arraySize;
        inputVariables.serializedProperty.arraySize++;
        inputVariables.index = index;
        var element = inputVariables.serializedProperty.GetArrayElementAtIndex(index);
        element.objectReferenceValue = iv;
        serializedObject.ApplyModifiedProperties();
    }
}