using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public static class Randomiser
{
    public static float[,] Randomise(InputVariable[] ivs, int sampleSize)
    {
        var matrix = new float[sampleSize, ivs.Length];

        for (int j = 0; j < ivs.Length; j++)
        {
            var iv = ivs[j];
            
            if (iv is CategoricalInputVariable)
            {
                var civ = (CategoricalInputVariable)iv;
                var block = civ.Sample();
                float[] column;
                if (civ.controlSpacing)
                {
                    // TODO move exceptions to editor

                    if (civ.minSpacing.Where(f => f > 0).Count() > 1) throw new System.NotSupportedException("Currently spacing only supported for one variable");

                    var spacing = civ.minSpacing.First(f => f > 0);
                    var idx = civ.minSpacing.IndexOf(spacing);
                    var spaced = civ.variables[idx];

                    if (civ.weights[idx] > 1) throw new System.ArgumentException("Spacing currently only supported for weight 1 of spaced variable");
                    if (spacing > 1f * civ.weights.Where((i, w) => i != idx).Sum() / civ.weights[idx]) throw new System.ArgumentException("Spacing is impossibly large given weights of other variables");

                    var buffer = spacing;
                    var colList = new List<float>();

                    for (int i = 0; i < sampleSize / block.Length + 1; i++)
                    {
                        do
                        {
                            block.Shuffle();
                        } while (block.IndexOf(spaced) < buffer);
                        buffer = spacing - (block.Length - 1 - block.IndexOf(spaced));
                        colList.AddRange(block);
                    }
                    
                    column = colList.SkipLast(block.Length - sampleSize % block.Length)
                                    .ToArray();
                }
                else
                {
                    column = Enumerable.Repeat(block, sampleSize / block.Length + 1)
                            .SelectMany(b => b.Shuffled())
                            .SkipLast(block.Length - sampleSize % block.Length)
                            .ToArray();
                }
                
                column.ForEach((i, x) => matrix[i,j] = x);                
            }
        }
        return matrix;
    }
}