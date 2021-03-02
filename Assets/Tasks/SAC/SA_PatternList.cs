using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Custom/Tasks/SA/PatternList")]
public class SA_PatternList : ScriptableObject
{
    public List<Pattern> patterns;

    [System.Serializable]
    public struct Pattern
    {
        public List<FloatVariable> variables;
    }
}