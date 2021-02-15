using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="Custom/Variable/StringVariable")]
public class StringVariable : Variable
{
    public string Value;
    void OnEnable() => Value = PlayerPrefs.GetString(GetInstanceID().ToString(), Value);
    public void SaveValue(string val = null)
    {
        Value = val ?? Value;
        PlayerPrefs.SetString(GetInstanceID().ToString(), Value);
    }

    public override string ToString() => Value;
}