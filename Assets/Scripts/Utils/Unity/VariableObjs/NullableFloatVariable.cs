using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/Variable/NullableFloatVariable")]
public class NullableFloatVariable : Variable
{
    public float? Value;
    public override string ToString()
    {
        return Value.HasValue ? Value.Value.ToString() : "NaN";
    }
}