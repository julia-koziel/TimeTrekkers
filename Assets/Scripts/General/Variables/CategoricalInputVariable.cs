using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName="Custom/Input Variable/CategoricalInputVariable")]
public class CategoricalInputVariable : InputVariable
{
    public int[] weights;
    public FloatVariable[] variables;
    public bool controlSpacing;
    public float[] minSpacing;
    public override float[] Sample(int? sampleSize = null)
    {
        var size = sampleSize ?? (weighted ? weights.Sum() : variables.Length);
        
        var block = variables.Select((v, i) => Enumerable.Repeat(v.Value, weights[i])).SelectMany(v => v).ToArray();

        return block.RepeatForSize(size).ToArray();
    }

    public static implicit operator int(CategoricalInputVariable civ) => (int)civ.Value;
}