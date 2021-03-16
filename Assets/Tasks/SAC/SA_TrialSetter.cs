using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using EasyButtons;

[CreateAssetMenu(menuName = "Custom/Tasks/SA/TrialSetter")]
public class SA_TrialSetter : TrialSetter
{
    public SA_PatternList patternList;
    public SA_PatternList PatternList { get; set; }
    public CategoricalInputVariable trialType;
    float FOIL = 0, TARGET = 1, PATTERN = 2;
    public CategoricalInputVariable stimuliIds;
    public int minSpacing;
    [Header("1 Pattern per block")]
    public int blockLength;
    bool debug = true;

    public override void SetTrialMatrix()
    {
        int nTrials = inputVariablesManager.nTrials;
        var ivs = inputVariablesManager.inputVariables;
        int nIvs = ivs.Length;
        var matrix = new float[nTrials, nIvs];

        var columnBuilder = new List<float>();
        var idColumnBuilder = new List<float>();

        float[] block = new float[blockLength];
        float[] idBlock;
        var idVars = stimuliIds.variables.Select(v => v.Value).ToArray();

        int nBlocks = nTrials / blockLength + 1;
        int lastIndexOfBlock = blockLength - 1;
        var buffer = minSpacing;

        var patterns = patternList.patterns.Select(p => p.variables.Select(v => v.Value).ToList()).ToList();
        int nPatterns = patterns.Count();

        var rng = getRng(); // gets fixed randomiser if specified, otherwise new Random Number Generator (rng)

        // Build trial matrix 1 block at a time
        for (int i = 0; i < nBlocks; i++)
        {
            block.ForEach(x => FOIL);
            idBlock = block.Select(f => idVars[rng.Next(idVars.Length)]).ToArray();

            var patternIndex = rng.Next(nPatterns);
            var pattern = patterns[patternIndex];
            var patternLength = pattern.Count;
            buffer += patternLength - 1;

            var targetIndex = rng.Next(buffer, blockLength);
            block[targetIndex] = TARGET;
            idBlock[targetIndex] = pattern.Last();

            // Fill pattern in reverse
            for (int j = 1; j < patternLength; j++)
            {
                block[targetIndex - j] = PATTERN;
                idBlock[targetIndex - j] = pattern[pattern.Count - 1 - j];
            }
            columnBuilder.AddRange(block);
            idColumnBuilder.AddRange(idBlock);
            // Calculate buffer needed at beginning of next block
            buffer = minSpacing - (lastIndexOfBlock - targetIndex);
            buffer = Mathf.Max(0, buffer);
        }

        // Cut off extra trials to fit nTrials
        var trialTypeColumn = columnBuilder.SkipLast(blockLength - nTrials % blockLength).ToArray();
        var idColumn = idColumnBuilder.SkipLast(blockLength - nTrials % blockLength).ToArray();

        // If target cut off from end, change remaining pattern to foil
        for (int i = trialTypeColumn.Length - 1; i >= 0; i--)
        {
            if (trialTypeColumn[i] == PATTERN)
            {
                trialTypeColumn[i] = FOIL;
            }
            else break;
        }
        // Helper function, checks
        // - first num matches first num of pattern
        // - enough nums after for pattern to be possible
        // - pattern isn't intentional
        // - pattern is a match
        bool matchesPattern(List<float> pattern, int index)
        {
            return idColumn[index] == pattern[0] &&
                    index + pattern.Count - 1 < trialTypeColumn.Length &&
                    trialTypeColumn[index + pattern.Count - 1] != TARGET &&
                    idColumn.Slice(index, index + pattern.Count).SequenceEqual(pattern);
        }
        // to avoid re-making pattern
        var valsToAvoid = new List<float>[idColumn.Length];
        valsToAvoid.ForEach(l => new List<float>());
    
        bool columnContainsPattern;
        do
        {
            columnContainsPattern = false;
            for (int i = 0; i < idColumn.Length; i++)
            {
                bool sequenceContainsPattern; ;
                do
                {
                    sequenceContainsPattern = false;
                    patterns.Where(p => matchesPattern(p, i)).ForEach(pattern =>
                    {
                        // ensures double-check after changes
                        sequenceContainsPattern = true;
                        columnContainsPattern = true;

                        for (int j = i; j < i + pattern.Count; j++)
                        {
                            if (trialTypeColumn[j] == FOIL) // only replace foils
                            {
                                var oldVal = idColumn[j];
                                valsToAvoid[j].Add(oldVal);
                                var possVals = idVars.Except(valsToAvoid[j]).ToArray();
                                idColumn[j] = possVals[rng.Next(possVals.Length)];
                                break; // break when one val in accidental pattern changed
                            }
                        }
                    });
                } while (sequenceContainsPattern);
            }
        } while (columnContainsPattern);

        if (debug)
        {
            // Debug.Log("\n".Join(valsToAvoid.Select((list, i) => new {list, i}).Where(pair => pair.list.Count > 0).Select(pair => $"{pair.i}: {", ".Join(pair.list)}")));
            var colours = new string[] {"red", "yellow", "orange", "pink", "grey", "green", "blue", "magenta", "black"};
            string colourString(float num) => $"<color={colours[(int)num]}>{num}</color>";

            Debug.Log($"patterns: {"||".Join(patterns.Select(p => " ".Join(p.Select(f => colourString(f)))))}");
            var patternString = " ".Join(trialTypeColumn.Select((f, i) => {
                var col = colourString(idColumn[i]);
                if (f > 0) col = $"<b><i>{col}</i></b>";
                return col;
            }));
            Debug.Log(patternString);
        }

        var iTrialType = ivs.IndexOf(trialType);
        var iId = ivs.IndexOf(stimuliIds);
        trialTypeColumn.ForEach((i, x) => matrix[i, iTrialType] = x);
        idColumn.ForEach((i, x) => matrix[i, iId] = x);

        inputVariablesManager.TrialMatrix = matrix;
    }

    [Button]
    public void Test(int nTrials)
    {
        inputVariablesManager.nTrials.Value = nTrials;
        debug = true;
        SetTrialMatrix();
        debug = false;
    }
}