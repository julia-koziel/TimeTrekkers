using System.Linq;

public class GridSpec
{
    private int nRows;
    private int nCols;
    private int[] widthRatios;
    private int[] heightRatios;
    private float[] bottoms;
    private float[] tops;
    private float[] lefts;
    private float[] rights;

    public GridSpec(int nRows, int nCols, int[] widthRatios = null, int[] heightRatios = null)
    {
        this.nRows = nRows;
        this.nCols = nCols;
        // TODO check length of ratios
        this.widthRatios = widthRatios ?? (new int[nCols]).ForEach(_ => 1);
        this.heightRatios = heightRatios ?? (new int[nRows]).ForEach(_ => 1);

        generateSubplots();
    }

    private void generateSubplots()
    {
        bottoms = new float[nRows];
        tops = new float[nRows];
        lefts = new float[nCols];
        rights = new float[nCols];

        float currentHeight = 0;
        float hStep = 1f / heightRatios.Sum();

        for (int i = 0; i < nRows; i++)
        {
            bottoms[i] = currentHeight;
            tops[i] = currentHeight + heightRatios[i] * hStep;
            currentHeight = tops[i];
        }

        float currentWidth = 0;
        float wStep = 1f / widthRatios.Sum();

        for (int i = 0; i < nCols; i++)
        {
            lefts[i] = currentWidth;
            rights[i] = currentWidth + widthRatios[i] * wStep;
            currentWidth = rights[i];
        }
    }

    // return width and heigh start and end as proportions
    public (float left, float right, float bottom, float top) getSubplot(int row, int col)
    {
        return (lefts[col], rights[col], bottoms[row], tops[row]);
    }
}