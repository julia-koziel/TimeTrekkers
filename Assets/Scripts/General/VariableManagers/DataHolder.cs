using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/VariableManager/DataHolder")]
public class DataHolder : ScriptableObject
{
    public Variable[] variables;
    public string[] headers;
    public string[] getVariables()
    {
        return variables.Select(v => v.ToString()).ToArray();
    }
    public int nVariables { get => variables.Length; }
    public string[] missingRow { get => variables.Select(v => "NaN").ToArray(); }
    // Declare the method signature of the delegate to call.
     // For a void method with no parameters you could just use System.Action.
    public delegate void RepaintAction();
 
     // Declare the event to which editor code will hook itself.
    public event RepaintAction WantRepaint;
 
     // This private method will invoke the event and thus the attached editor code.
    public void Repaint()
    {
        // If no handlers are attached to the event then don't invoke it.
        if (WantRepaint != null)
        {
            WantRepaint();
        }
    }
}

