using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/Variable/FloatVariable")]
public class FloatVariable : Variable
{
    public float Value;
    public override string ToString()
    {
        return Value.ToString();
    }
    void OnEnable() => Value = PlayerPrefs.GetFloat(GetInstanceID().ToString(), Value);
    public void SaveValue(float? val = null)
    {
        Value = val ?? Value;
        PlayerPrefs.SetFloat(GetInstanceID().ToString(), Value);
    }
    public static implicit operator float(FloatVariable v) => v.Value;
}