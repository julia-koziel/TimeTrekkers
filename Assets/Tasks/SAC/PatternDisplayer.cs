using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatternDisplayer : MonoBehaviour
{
    public SA_PatternList patternList { get; set; }
    public CategoricalInputVariable stimulusId;
    public Stimulus[] stimuli;
    public int nPatterns;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var patterns = patternList.patterns.Select(p => p.variables.Select(v => v.Value).ToList()).ToList();
        int nPatterns = patterns.Count();

        var patternIndex = Random.Range(0, nPatterns);
        var pattern = patterns[patternIndex];
        var patternLength = pattern.Count;

        var first = pattern.First();
        var second = pattern.Last();

        var stimulusfirst = stimuli.Where(s => s.id == pattern.First());
        var stimulussecond = stimuli.Where(s => s.id == pattern.Last());
    }
   
}

