using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public abstract class TrialSetter : ScriptableObject
{
    public InputVariablesManager inputVariablesManager;
    public abstract void SetTrialMatrix();
}