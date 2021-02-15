using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/Variable/IntVariable")]
public class IntVariable : FloatVariable
{
    public int intValue
    {
        set => base.Value = value;
        get => (int)base.Value;
    }

    public override string ToString()
    {
        return intValue.ToString();
    }
    public static IntVariable operator +(IntVariable intVar) => intVar;
    public static int operator -(IntVariable intVar) => -intVar.intValue;
    public static IntVariable operator ++(IntVariable intVar)
    {
        intVar.intValue++;
        return intVar;
    }
    public static IntVariable operator --(IntVariable intVar)
    {
        intVar.intValue--;
        return intVar;
    }
    public static implicit operator int(IntVariable intVar) => intVar.intValue;
    public static implicit operator float(IntVariable intVar) => intVar.intValue;
}