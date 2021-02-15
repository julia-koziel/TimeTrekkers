
public class DiscreteInputVariable : InputVariable
{
    public bool useGenerator;
    public float[] values;
    public float[] weights;
    public float start;
    public float stop;
    public float step;
    // Distribution?
    public override float[] Sample(int? sampleSize)
    {
        return new float[sampleSize.Value];
    }
}