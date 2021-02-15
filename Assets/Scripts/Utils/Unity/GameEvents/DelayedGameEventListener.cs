using UnityEngine;

public class DelayedGameEventListener : GameEventListener 
{
    public float delay;
    public override void OnEventRaised() => this.In(delay).Call(() => Response.Invoke());
}