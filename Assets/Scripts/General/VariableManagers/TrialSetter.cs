using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public abstract class TrialSetter : ScriptableObject
{
    public InputVariablesManager inputVariablesManager;
    public RandomisationSeed[] randomisationSeeds;
    public abstract void SetTrialMatrix();
    public System.Random getRng()
    {
        // No seeds specified - randomisation not fixed
        if (randomisationSeeds.Length == 0) return new System.Random();
        // If all seeds used, start from beginning
        if (!randomisationSeeds.Any(rs => !rs.used)) randomisationSeeds.ForEach(rs => rs.used = false);
        // Use first unused seed
        var rand = randomisationSeeds.First(rs => !rs.used);
        rand.used = true;
        return new System.Random(rand.seed);
    }
}
[System.Serializable]
public class RandomisationSeed
{
    public int seed;
    public bool used;
}