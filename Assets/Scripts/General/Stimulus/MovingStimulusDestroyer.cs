using UnityEngine;

[RequireComponent(typeof(StimulusMover))]
public class MovingStimulusDestroyer : MonoBehaviour 
{
    StimulusMover stimulusMover;
    void Awake() 
    {
        stimulusMover = GetComponent<StimulusMover>();
    }
    public void DestroyIfOffScreen() 
    {
        if (!stimulusMover.IsMoving) Destroy(this.gameObject);
    }

    public void Destroy() => Destroy(this.gameObject);

    bool IsOffScreen()
    {
        var direction = (stimulusMover.endPosition - stimulusMover.startPosition).normalized;
        return (stimulusMover.endPosition - transform.position).normalized != direction;
    }
}