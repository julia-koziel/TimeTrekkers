using UnityEngine;

public class ItiDelayer : MonoBehaviour 
{
    public FloatVariable itiTimer;
    public float resetTime;
    public void DelayIti()
    {
        itiTimer.Value = resetTime;
    }
}