using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using ST = Stage.StageType;
using System;

[CustomEditor(typeof(StageList))]
[CanEditMultipleObjects]
public class StageListEditor : Editor
{
    ReorderableList stages;
    string[] stageNames 
    { 
        get 
        { 
            var size = stages.serializedProperty.arraySize;
            if (size == 0) return new string[] {"No Stages Added"};
            else
            {
                return (new string[size]).Select((s, i) => stages.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("stageName").stringValue).ToArray();
            }
        } 
    }
    int[] stageIds 
    { 
        get 
        { 
            var size = stages.serializedProperty.arraySize;
            if (size == 0) return new int[] {};
            else
            {
                return (new int[size]).Select((s, i) => stages.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("stageId").intValue).ToArray();
            }
        } 
    }
    SerializedProperty deletedIds;

    void OnEnable() 
    {
        stages = CreateList(serializedObject, serializedObject.FindProperty("stages"));
        deletedIds = serializedObject.FindProperty("deletedIds");
    }

    ReorderableList CreateList (SerializedObject obj, SerializedProperty prop)
    {
        ReorderableList list = new ReorderableList(obj, prop, true, true, true, true);

        List<float> heights = new List<float>(prop.arraySize);

        list.drawElementCallback = (rect, index, active, focused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            
            float heightUnit = EditorGUIUtility.singleLineHeight * 1.25f;
            float heightMargin = EditorGUIUtility.singleLineHeight * 0.125f;
            float height = heightUnit;
            rect.y += heightMargin;
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.x -= 15;
            rect.width += 15;

            var stageType = element.FindPropertyRelative("stageType");
            var typeName = stageType.enumNames[stageType.enumValueIndex];
            var stageName = element.FindPropertyRelative("stageName");

            var rect1 = new Rect(rect.x + 15, rect.y, rect.width / 2 - 15, rect.height);
            var rect2 = new Rect(rect.x + rect.width / 2, rect.y, rect.width / 2, rect.height);

            EditorGUI.LabelField(rect1, new GUIContent($"Name ({typeName})"));
            EditorGUI.PropertyField(rect2, stageName, GUIContent.none);

            if (active)
            {
                
                height += heightUnit;
                rect.y += heightUnit;
                if (stageType.IsEnumVal(typeof(ST), ST.Basic, ST.End))
                {
                    var rects = rect.DivideHorizontally(2);
                    EditorGUI.LabelField(rects[0], "Stage GameObject");
                    EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("stageGameObject"), GUIContent.none); 
                }
                else if (stageType.IsEnumVal(typeof(ST), ST.Demo, ST.Parent, ST.Trials))
                {
                    var rects = rect.DivideHorizontally(2);
                    EditorGUI.LabelField(rects[0], "No. Trials");
                    EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("nTrials"), GUIContent.none); 
                    height += heightUnit;
                    rect.y += heightUnit;
                    rects = rect.DivideHorizontally(2);
                    EditorGUI.LabelField(rects[0], "ITI");
                    EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("iti"), GUIContent.none); 
                    height += heightUnit;
                    rect.y += heightUnit;
                    rects = rect.DivideHorizontally(2);
                    EditorGUI.LabelField(rects[0], "Stimuli Manager");
                    EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("stageGameObject"), GUIContent.none);

                    height += heightUnit;
                    rect.y += heightUnit;
                    rects = rect.DivideHorizontally(2);

                    if (stageType.IsEnumVal(typeof(ST), ST.Demo))
                    {
                        var hasDemoProtocol = element.FindPropertyRelative("hasDemoProtocol");
                        hasDemoProtocol.boolValue = EditorGUI.ToggleLeft(rects[0], "Demo Protocol", hasDemoProtocol.boolValue);
                        if (hasDemoProtocol.boolValue)
                        {
                            EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("demoProtocol"), GUIContent.none);
                        }
                    }
                    else
                    {
                        var hasPresetMatrix = element.FindPropertyRelative("hasPresetMatrix");
                        hasPresetMatrix.boolValue = EditorGUI.ToggleLeft(rects[0], "Preset Trial Matrix", hasPresetMatrix.boolValue);
                        if (hasPresetMatrix.boolValue) 
                        {
                            EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("presetMatrix"), GUIContent.none);
                        }
                    }

                    

                    if (stageType.IsEnumVal(typeof(ST), ST.Trials))
                    {
                        height += heightUnit;
                        rect.y += heightUnit;
                        rects = rect.DivideHorizontally(2);
                        EditorGUI.LabelField(rects[0], "Data Holder");
                        EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("dataHolder"), GUIContent.none); 
                        height += heightUnit;
                        rect.y += heightUnit;
                        rects = rect.DivideHorizontally(2);

                        var hasOpeningAudio = element.FindPropertyRelative("hasOpeningAudio");
                        hasOpeningAudio.boolValue = EditorGUI.ToggleLeft(rects[0], "Opening Audio", hasOpeningAudio.boolValue);
                        if (hasOpeningAudio.boolValue) 
                        {
                            EditorGUI.PropertyField(rects[1], element.FindPropertyRelative("openingAudio"), GUIContent.none);
                        }

                        height += heightUnit;
                        rect.y += heightUnit;

                        var hasRepeatStage = element.FindPropertyRelative("hasRepeatStage");
                        var repeatStage = element.FindPropertyRelative("repeatStage");

                        rect1 = new Rect(rect.x, rect.y, rect.width / 2, rect.height);
                        rect2 = new Rect(rect.x + rect.width / 2 + 5, rect.y, rect.width / 8, rect.height);
                        var rect3 = new Rect(rect.x + 5 * rect.width / 8 + 10, rect.y, 3 * rect.width / 8 - 10, rect.height);

                        hasRepeatStage.boolValue = EditorGUI.ToggleLeft(rect1, "On Repeat", hasRepeatStage.boolValue);
                        if (hasRepeatStage.boolValue)
                        {
                            var repeatIndex = stageIds.IndexOf(repeatStage.intValue);
                            if (repeatIndex < 0) repeatIndex = index;

                            EditorGUI.LabelField(rect2, "Go to");
                            repeatIndex = EditorGUI.Popup(rect3, repeatIndex, stageNames);
                            
                            repeatStage.intValue = stageIds[repeatIndex];
                        }
                    } 
                }
                else if (stageType.IsEnumVal(typeof(ST), ST.UI))
                {
                    rect1 = new Rect(rect.x, rect.y, rect.width / 2, rect.height);
                    rect2 = new Rect(rect.x + rect.width / 2, rect.y, rect.width / 4, rect.height);
                    var rect3 = new Rect(rect.x + 3 * rect.width / 4, rect.y, rect.width / 4, rect.height);
                    var hasTopText = element.FindPropertyRelative("hasTopText");
                    var hasBottomText = element.FindPropertyRelative("hasBottomText");

                    EditorGUI.LabelField(rect1, "Textbox");
                    hasTopText.boolValue = EditorGUI.ToggleLeft(rect2, "Top", hasTopText.boolValue);
                    hasBottomText.boolValue = EditorGUI.ToggleLeft(rect3, "Bottom", hasBottomText.boolValue);

                    serializedObject.ApplyModifiedProperties();

                    if (hasTopText.boolValue)
                    {
                        height += heightUnit; // was 2*
                        rect.y += heightUnit;
                        // var doubleRect = new Rect(rect.x, rect.y, rect.width, 2 * rect.height);
                        var topText = element.FindPropertyRelative("topText");
                        EditorGUI.PropertyField(rect, topText, GUIContent.none);
                        // rect.y += heightUnit;
                    }
                    if (hasBottomText.boolValue)
                    {
                        height += heightUnit;
                        rect.y += heightUnit;

                        EditorGUI.PropertyField(rect, element.FindPropertyRelative("bottomText"), GUIContent.none);
                    }

                    height += heightUnit;
                    rect.y += heightUnit;

                    rect1 = new Rect(rect.x, rect.y, rect.width / 4, rect.height);
                    rect2 = new Rect(rect.x + rect.width / 4, rect.y, rect.width / 4, rect.height);
                    rect3 = new Rect(rect.x + 2 * rect.width / 4, rect.y, rect.width / 4, rect.height);
                    var rect4 = new Rect(rect.x + 3 * rect.width / 4, rect.y, rect.width / 4, rect.height);

                    var hasRepeatStage = element.FindPropertyRelative("hasRepeatStage");
                    var hasContinueStage = element.FindPropertyRelative("hasContinueStage");
                    var hasCustomStage = element.FindPropertyRelative("hasCustomStage");

                    EditorGUI.LabelField(rect1, "Buttons");
                    hasRepeatStage.boolValue = EditorGUI.ToggleLeft(rect2, "Back", hasRepeatStage.boolValue);
                    hasCustomStage.boolValue = EditorGUI.ToggleLeft(rect3, "Custom", hasCustomStage.boolValue);
                    hasContinueStage.boolValue = EditorGUI.ToggleLeft(rect4, "Forward", hasContinueStage.boolValue);

                    if (hasRepeatStage.boolValue)
                    {
                        height += heightUnit;
                        rect.y += heightUnit;
                        rect1 = new Rect(rect.x, rect.y, rect.width / 8, rect.height);
                        rect2 = new Rect(rect.x + rect.width / 8, rect.y, 3 * rect.width / 8, rect.height);
                        rect3 = new Rect(rect.x + rect.width / 2 + 5, rect.y, rect.width / 8, rect.height);
                        rect4 = new Rect(rect.x + 5 * rect.width / 8 + 10, rect.y, 3 * rect.width / 8 - 10, rect.height);

                        var repeatText = element.FindPropertyRelative("repeatText");
                        var repeatStage = element.FindPropertyRelative("repeatStage");
                        
                        var repeatIndex = stageIds.IndexOf(repeatStage.intValue);
                        if (repeatIndex < 0) repeatIndex = index;

                        EditorGUI.LabelField(rect1, "Text");
                        EditorGUI.PropertyField(rect2, repeatText, GUIContent.none);
                        EditorGUI.LabelField(rect3, "Go to");
                        repeatIndex = EditorGUI.Popup(rect4, repeatIndex, stageNames);
                        
                        repeatStage.intValue = stageIds[repeatIndex];
                    }
                    else
                    {
                        element.FindPropertyRelative("repeatStage").intValue = element.FindPropertyRelative("stageId").intValue;
                    }

                    if (hasCustomStage.boolValue)
                    {
                        height += heightUnit;
                        rect.y += heightUnit;
                        rect1 = new Rect(rect.x, rect.y, rect.width / 8, rect.height);
                        rect2 = new Rect(rect.x + rect.width / 8, rect.y, 3 * rect.width / 8, rect.height);
                        rect3 = new Rect(rect.x + rect.width / 2 + 5, rect.y, rect.width / 8, rect.height);
                        rect4 = new Rect(rect.x + 5 * rect.width / 8 + 10, rect.y, 3 * rect.width / 8 - 10, rect.height);

                        var customText = element.FindPropertyRelative("customText");
                        var customStage = element.FindPropertyRelative("customStage");
                        
                        var customIndex = stageIds.IndexOf(customStage.intValue);
                        if (customIndex < 0) customIndex = index;

                        EditorGUI.LabelField(rect1, "Text");
                        EditorGUI.PropertyField(rect2, customText, GUIContent.none);
                        EditorGUI.LabelField(rect3, "Go to");
                        customIndex = EditorGUI.Popup(rect4, customIndex, stageNames);
                        
                        customStage.intValue = stageIds[customIndex];
                    }
                    else
                    {
                        element.FindPropertyRelative("customStage").intValue = element.FindPropertyRelative("stageId").intValue;
                    }

                    if (hasContinueStage.boolValue)
                    {
                        height += heightUnit;
                        rect.y += heightUnit;
                        rect1 = new Rect(rect.x, rect.y, rect.width / 8, rect.height);
                        rect2 = new Rect(rect.x + rect.width / 8, rect.y, 3 * rect.width / 8, rect.height);
                        rect3 = new Rect(rect.x + rect.width / 2 + 5, rect.y, rect.width / 8, rect.height);
                        rect4 = new Rect(rect.x + 5 * rect.width / 8 + 10, rect.y, 3 * rect.width / 8 - 10, rect.height);

                        var continueText = element.FindPropertyRelative("continueText");
                        var continueStage = element.FindPropertyRelative("continueStage");
                        
                        var continueIndex = stageIds.IndexOf(continueStage.intValue);
                        if (continueIndex < 0) continueIndex = index;

                        EditorGUI.LabelField(rect1, "Text");
                        EditorGUI.PropertyField(rect2, continueText, GUIContent.none);
                        EditorGUI.LabelField(rect3, "Go to");
                        continueIndex = EditorGUI.Popup(rect4, continueIndex, stageNames);
                        
                        continueStage.intValue = stageIds[continueIndex];
                    }
                    else
                    {
                        element.FindPropertyRelative("continueStage").intValue = element.FindPropertyRelative("stageId").intValue;
                    }

                    height += heightUnit;
                    rect.y += heightUnit;

                    var hasMaxViewings = element.FindPropertyRelative("hasMaxViewings");
                    var maxViewings = element.FindPropertyRelative("maxViewings");
                    var maxViewingsStage = element.FindPropertyRelative("maxViewingsStage");

                    rect1 = new Rect(rect.x, rect.y, 3 * rect.width / 8, rect.height);
                    rect2 = new Rect(rect.x + 3 * rect.width / 8, rect.y, rect.width / 8, rect.height);
                    rect3 = new Rect(rect.x + rect.width / 2 + 5, rect.y, rect.width / 8, rect.height);
                    rect4 = new Rect(rect.x + 5 * rect.width / 8 + 10, rect.y, 3 * rect.width / 8 - 10, rect.height);

                    hasMaxViewings.boolValue = EditorGUI.ToggleLeft(rect1, "Max Viewings", hasMaxViewings.boolValue);
                    if (hasMaxViewings.boolValue)
                    {
                        EditorGUI.PropertyField(rect2, maxViewings, GUIContent.none);
                        EditorGUI.LabelField(rect3, "Go to");
                        var maxViewingsIndex = stageIds.IndexOf(maxViewingsStage.intValue);
                        if (maxViewingsIndex < 0) maxViewingsIndex = index;
                        maxViewingsIndex = EditorGUI.Popup(rect4, maxViewingsIndex, stageNames);

                        maxViewingsStage.intValue = stageIds[maxViewingsIndex];
                    }
                    else maxViewings.intValue = 0;
                }
            }

            height += heightMargin;

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
            EditorGUI.LabelField(rect, "Stages");
        };

        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();

            foreach (string type in Enum.GetNames(typeof(ST)))
            {
                menu.AddItem(new GUIContent($"Create New/{type}"), false, () => { 
                    var newId = getNewStageId();
                    var index = l.serializedProperty.arraySize;
                    l.serializedProperty.arraySize++;
                    l.index = index;
                    var element = l.serializedProperty.GetArrayElementAtIndex(index);
                    var stageType = element.FindPropertyRelative("stageType");
                    stageType.enumValueIndex = stageType.enumNames.IndexOf(type);
                    element.FindPropertyRelative("stageName").stringValue = $"New {type} Stage";
                    element.FindPropertyRelative("stageId").intValue = newId;
                    element.FindPropertyRelative("repeatStage").intValue = newId;
                    element.FindPropertyRelative("customStage").intValue = newId;
                    element.FindPropertyRelative("continueStage").intValue = newId;
                    element.FindPropertyRelative("hasTopText").boolValue = false;
                    element.FindPropertyRelative("hasBottomText").boolValue = false;
                    element.FindPropertyRelative("hasRepeatStage").boolValue = false;
                    element.FindPropertyRelative("hasContinueStage").boolValue = false;
                    element.FindPropertyRelative("hasCustomStage").boolValue = false;
                    element.FindPropertyRelative("hasMaxViewings").boolValue = false;
                    element.FindPropertyRelative("hasPresetMatrix").boolValue = false;
                    element.FindPropertyRelative("hasDemoProtocol").boolValue = false;
                    element.FindPropertyRelative("hasOpeningAudio").boolValue = false;
                    serializedObject.ApplyModifiedProperties();
                });
            }
            
            menu.ShowAsContext();
        };

        list.onRemoveCallback = (ReorderableList l) => {
            var idx = l.index;
            var deletedId = stageIds[idx];
            var referencingIdxs = getIdxsOfStagesReferencingStage(deletedId);
            if (referencingIdxs.Length > 0 && !EditorUtility.DisplayDialog("Warning!", 
    	        "The following stages have references to this stage:\n\n" + 
                $"{", ".Join(referencingIdxs.Select(i => stageNames[i]))}\n\n" + 
                "Deleting this stage will reset all those references. Are you sure you want to continue?", 
                "Yes", "No"))
            {
                return;
            }
            else
            {
                resetReferencesTo(deletedId);
                l.index = -1;
                deletedIds.arraySize++;
                deletedIds.GetArrayElementAtIndex(deletedIds.arraySize-1).intValue = deletedId;
                l.serializedProperty.DeleteArrayElementAtIndex(idx);
                serializedObject.ApplyModifiedProperties();
            }
        };

        return list;
    }

    int getNewStageId()
    {
        if (deletedIds.arraySize > 0)
        {
            var newId = deletedIds.GetArrayElementAtIndex(0).intValue;
            Assert.IsFalse(stageIds.Contains(newId));
            deletedIds.DeleteArrayElementAtIndex(0);
            return newId;
        }
        else
        {
            var newId = stages.serializedProperty.arraySize;
            Assert.IsFalse(stageIds.Contains(newId));
            return newId;
        }
    }

    int[] getIdxsOfStagesReferencingStage(int id)
    {
        var idxs = new List<int>();
        var stages = this.stages.serializedProperty;
        for (int i = 0; i < stages.arraySize; i++)
        {
            if (stageIds[i] == id) continue;

            var stage = stages.GetArrayElementAtIndex(i);
            if (stage.FindPropertyRelative("repeatStage").intValue == id ||
                stage.FindPropertyRelative("customStage").intValue == id ||
                stage.FindPropertyRelative("continueStage").intValue == id)
            {
                idxs.Add(i);
            }
        }
        return idxs.ToArray();
    }

    void resetReferencesTo(int id)
    {
        var stages = this.stages.serializedProperty;
        for (int i = 0; i < stages.arraySize; i++)
        {
            var stage = stages.GetArrayElementAtIndex(i);

            var repeatStage = stage.FindPropertyRelative("repeatStage");
            var customStage = stage.FindPropertyRelative("customStage");
            var continueStage = stage.FindPropertyRelative("continueStage");

            if (repeatStage.intValue == id) repeatStage.intValue = stageIds[i];
            if(customStage.intValue == id) customStage.intValue = stageIds[i];
            if (continueStage.intValue == id) continueStage.intValue = stageIds[i];
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space();

        stages.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

    }

    private void clickHandler(object target) 
    {
        var iv = (InputVariable)target;
        var index = stages.serializedProperty.arraySize;
        stages.serializedProperty.arraySize++;
        stages.index = index;
        var element = stages.serializedProperty.GetArrayElementAtIndex(index);
        element.objectReferenceValue = iv;
        serializedObject.ApplyModifiedProperties();
    }
}