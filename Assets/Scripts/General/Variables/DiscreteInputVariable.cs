using System.Linq;
using System;
using System.Collections.Generic;
using MathNet.Numerics;
using UnityEngine;

[CreateAssetMenu(menuName="Custom/Input Variable/DiscreteInputVariable")]
public class DiscreteInputVariable : InputVariable
{
    public bool useGenerator;
    public float[] values;
    public float[] weights;
    public float start;
    public float step;
    public float stop;
    // Distribution?
    public override float[] Sample(int? sampleSize)
    {
        IEnumerable<float> block;

        if (useGenerator) block = Generate.LinearRange(start, step, stop).Select(d => (float)d);
        else throw new NotImplementedException();

        var size = sampleSize ?? block.Count();

        return block.RepeatForSize(size).ToArray();
    }
}