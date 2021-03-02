using System;
using UnityEngine;
// TODO try to change to subclasses with Type parameter
[Serializable]
public class Stage : IComparable
{
    public enum StageType { UI, Demo, Parent, Trials, Basic, End, Custom, Prompts }
    public StageType stageType;
    public string stageName;
    public int stageId;
    public int nViews;
    public bool hasTopText, hasBottomText, hasRepeatStage, hasContinueStage, hasCustomStage;
    public TranslatableString topText, bottomText, repeatText, customText, continueText;
    public int repeatStage, customStage, continueStage, maxViewingsStage;
    public bool hasMaxViewings;
    public int maxViewings;
    public bool waitForParent { 
        get
        {
            if (!_waitForParent.HasValue) _waitForParent = stageType == StageType.UI;
            return _waitForParent.Value;
        }
        set => _waitForParent = value;
    }
    bool? _waitForParent;
    public int nTrials;
    public float iti;
    public DataHolder dataHolder;
    public bool hasPresetMatrix;
    public TrialMatrix presetMatrix;
    public bool hasTrialSetter;
    public TrialSetter trialSetter;
    public bool hasDemoProtocol;
    public DemoProtocol demoProtocol;
    public bool hasOpeningAudio;
    public TranslatableAudioClip openingAudio;
    public GameObject stageGameObject;
    public int nPrompts;
    public TranslatableString[] prompts;
    public int CompareTo(System.Object obj)
    {
        if (obj == null) return 1;

        Stage otherStage = obj as Stage;
        if (otherStage != null)
            return this.stageId.CompareTo(otherStage.stageId);
        else
           throw new ArgumentException("Object is not a Stage");
    }
}
