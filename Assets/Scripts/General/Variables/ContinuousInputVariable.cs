
public class ContinuousInputVariable : InputVariable
{
    public float[] weights;
    public FloatVariable[] variables;
    public override float[] Sample(int? sampleSize)
    {
        return new float[sampleSize.Value];
    }
}