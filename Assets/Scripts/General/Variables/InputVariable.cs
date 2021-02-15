
public abstract class InputVariable : FloatVariable
{
    public bool weighted;
    public abstract float[] Sample(int? size);
}