using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MenuTest : MonoBehaviour
{
    // Add a menu item to create custom GameObjects.
    // Priority 1 ensures it is grouped with the other menu items of the same kind
    // and propagated to the hierarchy dropdown and hierarchy context menus.
    [MenuItem("GameObject/UI/Button Content Fit", false, 10)]
    static void CreateContentFitButton(MenuCommand menuCommand)
    {
        EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
        var button = Selection.activeGameObject;
        var hlg = button.AddComponent<HorizontalLayoutGroup>();
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childForceExpandHeight = true;
        hlg.childForceExpandWidth = true;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.padding = new RectOffset(10, 10, 5, 5);
        var csf = button.AddComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        button.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.black;
        // // Create a custom game object
        // GameObject go = new GameObject("Custom Game Object");
        // // Ensure it gets reparented if this was a context click (otherwise does nothing)
        // GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // // Register the creation in the undo system
        // Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        // Selection.activeObject = go;
    }

    [MenuItem("GameObject/UI/Button Content Fit Translatable", false, 10)]
    static void CreateTranslatableContentFitButton(MenuCommand menuCommand)
    {
        EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
        var button = Selection.activeGameObject;
        var hlg = button.AddComponent<HorizontalLayoutGroup>();
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childForceExpandHeight = true;
        hlg.childForceExpandWidth = true;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.padding = new RectOffset(10, 10, 5, 5);
        var csf = button.AddComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var child = button.transform.GetChild(0).gameObject;
        DestroyImmediate(child.GetComponent<Text>());
        var text = child.AddComponent<TranslatableText>();
        text.color = Color.black;
        var guids = AssetDatabase.FindAssets("Menlo Regular t:Font");
        if (guids.Length == 1)
        {
            text.font = (Font)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(Font));
        }
        text.alignment = TextAnchor.MiddleCenter;
        // // Create a custom game object
        // GameObject go = new GameObject("Custom Game Object");
        // // Ensure it gets reparented if this was a context click (otherwise does nothing)
        // GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // // Register the creation in the undo system
        // Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        // Selection.activeObject = go;
    }
    [MenuItem("GameObject/UI/Image Button Content Fit Translatable", false, 10)]
    static void CreateTranslatableContentFitImageButton(MenuCommand menuCommand)
    {
        EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
        var button = Selection.activeGameObject;
        var hlg = button.AddComponent<HorizontalLayoutGroup>();
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childForceExpandHeight = false;
        hlg.childForceExpandWidth = true;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.padding = new RectOffset(10, 10, 5, 5);
        var csf = button.AddComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var child = button.transform.GetChild(0).gameObject;
        DestroyImmediate(child.GetComponent<Text>());
        var text = child.AddComponent<TranslatableText>();
        text.color = Color.black;
        var guids = AssetDatabase.FindAssets("Menlo Regular t:Font");
        if (guids.Length == 1)
        {
            text.font = (Font)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(Font));
        }
        text.alignment = TextAnchor.MiddleCenter;

        var image = new GameObject("Image");
        image.AddComponent<Image>();
        image.transform.parent = button.transform;
        // // Create a custom game object
        // GameObject go = new GameObject("Custom Game Object");
        // // Ensure it gets reparented if this was a context click (otherwise does nothing)
        // GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // // Register the creation in the undo system
        // Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        // Selection.activeObject = go;
    }
}