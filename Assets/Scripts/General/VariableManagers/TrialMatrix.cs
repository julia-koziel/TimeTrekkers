using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

[CreateAssetMenu(menuName="Custom/Protocol/TrialMatrix")]
public class TrialMatrix : ScriptableObject 
{
    public InputVariablesManager inputVariablesManager;
    public bool shuffle = false;
    public RowData[] rowArray;
    public float[,] trialMatrix;

    [System.Serializable]
    public struct RowData
    {
        public float[] row;
    }
    public void SetTrialMatrix()
    {
        if (rowArray == null || rowArray.Length == 0)
            throw new System.Exception("This matrix is empty, so cannot be used to set an InputVariablesManager trialMatrix");

        Assert.IsTrue(rowArray.All(r => r.row.Length == inputVariablesManager.inputVariables.Length));

        var matrix = new float[rowArray.Length, rowArray[0].row.Length];

        if (shuffle) rowArray.Shuffle();

        rowArray.ForEach((i, r) => r.row.ForEach((j, x) => matrix[i,j] = x));

        inputVariablesManager.TrialMatrix = matrix;
    }
}