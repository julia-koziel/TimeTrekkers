using UnityEngine;
using UnityEditor;
using System.Linq;

public class DebugFocuser : MonoBehaviour 
{
#if UNITY_EDITOR
    EditorWindow gameWindow;
    
    void Awake() 
    {
        gameWindow = EditorWindow.focusedWindow;
    }

    public void FocusGameWindow() 
    {
        var debugWindow = EditorWindow.focusedWindow;
        EditorWindow[] allWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
        var inspectorWindow = allWindows.FirstOrDefault(w => w.titleContent.text == "Inspector" && w != debugWindow);
        if (inspectorWindow != default(EditorWindow)) inspectorWindow.Focus();
        gameWindow.Focus();
    }
# endif
}