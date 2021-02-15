using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DebugResetter : MonoBehaviour
{
#if UNITY_EDITOR
    public GameEvent stageEnd;
    bool hasReset = false;

    void OnEnable() 
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    void OnDisable() {
        EditorApplication.playModeStateChanged -= LogPlayModeState;
    }

    IEnumerator Reset() 
    {
        stageEnd.Raise();
        yield return new WaitForSeconds(1);
        EditorApplication.ExitPlaymode();
    }

    private void LogPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode && !hasReset)
        {
            EditorApplication.EnterPlaymode();
            hasReset = !hasReset;
            StartCoroutine(Reset());
        }
        // if (state == PlayModeStateChange.EnteredEditMode) StartCoroutine(Reset());
    }
# endif
}
