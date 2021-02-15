using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/Variable/BoolVariable")]
public class BoolVariable : FloatVariable
{
    public bool boolValue
    {
        set => base.Value = value ? 1 : 0;
        get => base.Value == 1;
    }

    public override string ToString()
    {
        return ((int)base.Value).ToString();
    }

    public static implicit operator bool(BoolVariable b) => b.boolValue;
    public static bool operator true(BoolVariable b) => b.boolValue;
    public static bool operator false(BoolVariable b) => b.boolValue;
    public static bool operator !(BoolVariable b) => !b.boolValue;
}