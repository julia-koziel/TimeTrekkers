using UnityEngine;

[CreateAssetMenu(menuName="Custom/Protocol/Demo Protocol")]
public class DemoProtocol : ScriptableObject 
{
    public TrialMatrix trialMatrix;
    public float reactionTime = 0.5f;
    public enum AudioTiming {
        None, PreClick, OnClick, PostClick
    }
    public int[] trialNumbers;
    public bool[] wrongClicks;
    public TranslatableAudioClip[] preClickClips;
    public TranslatableAudioClip[] onClickClips;
    public TranslatableAudioClip[] postClickClips;

}