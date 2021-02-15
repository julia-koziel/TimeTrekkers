using UnityEngine;
using MathNet.Numerics;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;

[CustomEditor(typeof(DataHolder))]
[CanEditMultipleObjects]
public class DataHolderEditor : Editor
{
    SerializedProperty headers;
    ReorderableList dataList;
    Variable[] existingVariables;
    string[] existingVarNames;

    void OnEnable() 
    {
        string[] guids;

        // search for a ScriptObject called ScriptObj
        guids = AssetDatabase.FindAssets("t:Variable");
        existingVariables = guids.Select(g => (Variable)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(g), typeof(Variable))).ToArray();
        existingVarNames = existingVariables.Select(v => v.name).ToArray();

        var duplicates = existingVarNames.GroupBy(v => v).Where(v => v.Count() > 1).Select(v => v.Key).ToArray();
        
        for (int i = 0; i < existingVarNames.Length; i++)
        {
            
            if (existingVarNames[i].In(duplicates))
            {
                var path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(existingVariables[i])).Remove(0, 6);
                existingVarNames[i] += $"{path}";
            }
        }

        headers = serializedObject.FindProperty("headers");
        dataList = new ReorderableList(serializedObject, serializedObject.FindProperty("variables"), true, true, true, true);

        var dh = (DataHolder)target;
        dh.WantRepaint += this.Repaint;

        dataList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            // TODO handle empty
            rect.height = EditorGUIUtility.singleLineHeight;
            var header = headers.GetArrayElementAtIndex(index);
            var element = dataList.serializedProperty.GetArrayElementAtIndex(index);

            if (header.stringValue == String.Empty)
            {
                if (element.objectReferenceValue.name.Contains("Unnamed"))
                {
                    var placeholder = "Create with name: ";
                    var subrect = EditorGUI.PrefixLabel(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), new GUIContent(placeholder));
                    header.stringValue = EditorGUI.DelayedTextField(subrect, header.stringValue);
                    if (header.stringValue != "")
                    {
                        var asset = element.objectReferenceValue;
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(asset), header.stringValue);
                        element.objectReferenceValue.name = header.stringValue;
                        AssetDatabase.SaveAssets();
                        EditorUtility.FocusProjectWindow();
                        EditorGUIUtility.PingObject(asset);
                    }
                    return;
                }
                else
                {
                    header.stringValue = element.objectReferenceValue?.name ?? String.Empty;
                    serializedObject.ApplyModifiedProperties();
                }
            }

            var headerRect = new Rect(rect.x, rect.y, rect.width / 2 - 10, rect.height);
            var variableRect = new Rect(rect.x + rect.width / 2, rect.y, 35, rect.height);
            var valRect = new Rect(rect.x + rect.width / 2 + 35, rect.y, rect.width / 2 - 35, rect.height);

            EditorGUI.PropertyField(headerRect, header, GUIContent.none);
            EditorGUI.PropertyField(variableRect, element, GUIContent.none);
            EditorGUI.LabelField(valRect, element.objectReferenceValue.ToString());
        };

        dataList.drawHeaderCallback = (Rect rect) => {
            rect.height = EditorGUIUtility.singleLineHeight;
            var leftRect = new Rect(rect.x, rect.y, rect.width / 2, rect.height);
            var rightRect = new Rect(rect.x + rect.width / 2, rect.y, rect.width / 2, rect.height);
            EditorGUI.LabelField(leftRect, "Headers");
            EditorGUI.LabelField(rightRect, "Variables");
        };

        dataList.onRemoveCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize - 2;
            l.serializedProperty.arraySize--;
            headers.arraySize--;
            l.index = index;
            serializedObject.ApplyModifiedProperties();
        };

        dataList.onReorderCallbackWithDetails = (ReorderableList l, int oldIndex, int newIndex) => {
            headers.MoveArrayElement(oldIndex, newIndex);
        };

        dataList.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();

            Type varType = typeof(Variable);
            foreach (Type type in varType.Assembly.GetTypes().Where(t => t.IsSubclassOf(varType)))
            {
                menu.AddItem(new GUIContent($"Create New/{type.Name}"), false, () => { 
                    var index = l.serializedProperty.arraySize;
                    l.serializedProperty.arraySize++;
                    headers.arraySize++;
                    l.index = index;
                    var element = l.serializedProperty.GetArrayElementAtIndex(index);
                    var header = headers.GetArrayElementAtIndex(index);
                    var asset = ScriptableObject.CreateInstance(type);

                    AssetDatabase.CreateAsset(asset, $"Assets/Variables/Unnamed.asset");
                    AssetDatabase.SaveAssets();
                    EditorUtility.FocusProjectWindow();
                    element.objectReferenceValue = asset;
                    EditorGUIUtility.PingObject(asset);
                    header.stringValue = String.Empty;
                    serializedObject.ApplyModifiedProperties();
                });
            }
            
            
            for (int i = 0; i < existingVariables.Length; i++)
            {
                menu.AddItem(new GUIContent(existingVarNames[i]), false, clickHandler, existingVariables[i]);
            }
            
            menu.ShowAsContext();
        };
    }

    // And then detach on disable.
    void OnDisable()
    {
        var dh = (DataHolder)target;
        dh.WantRepaint -= this.Repaint;
    }

    private void clickHandler(object target) 
    {
        var iv = (Variable)target;
        var index = dataList.serializedProperty.arraySize;
        dataList.serializedProperty.arraySize++;
        headers.arraySize++;
        dataList.index = index;
        var element = dataList.serializedProperty.GetArrayElementAtIndex(index);
        element.objectReferenceValue = iv;
        var header = headers.GetArrayElementAtIndex(index);
        header.stringValue = element.objectReferenceValue.name;
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.Space();
        
        dataList.DoLayoutList();


        serializedObject.ApplyModifiedProperties();

    }
}