using UnityEngine;
using UnityEngine.SceneManagement;
using StageType = Stage.StageType;

[RequireComponent(typeof(AudioTranslator))]
public class GameLogic : MonoBehaviour
{
    public UILogic ui;
    public StageList stageList;
    public CsvReadWrite csv;
    public TrialRunner trialRunner;
    public CameraTransition transition;
    [Space(10)]
    public GameEvent stageReset;
    public IntVariable nTrials;
    public BoolVariable isParticipantsGo;
    public TranslatableString parentsGo;
    public TranslatableAudioClip parentsGoAudio;
    public TranslatableString wellDone;
    public TranslatableString finish;
    [Space(10)]
    public StringVariable stageName;
    public IntVariable nViews;
    public StringVariable uiTopText;
    public StringVariable uiBottomText;
    public StringVariable uiButton1;
    public StringVariable uiButton2;
    public StringVariable uiButton3;
    public StringVariable buttonPressed;
    AudioTranslator audioTranslator;
    Stage currentStage;
    Stage nextStage;
    void Awake()
    {
        stageList.stages.ForEach(s => s.nViews = 0);
        isParticipantsGo.boolValue = false;
        audioTranslator = GetComponent<AudioTranslator>();

        // Prep data for CsvReadWrite to log start time
        currentStage = StageList.emptyStage;
        LogData(currentStage.continueText);
        stageName.Value = "StartTime";
    }

    void Start()
    {
        StartStage(stageList.stages[0]);
    }

    void StartStage(Stage stage)
    {
        currentStage = stage;
        stage.nViews++;
        float audioLength = 0;

        switch (stage.stageType)
        {

            case StageType.Basic:
                stage.stageGameObject.SetActive(true);
                break;
            
            case StageType.UI:
                ui.topText = stage.hasTopText ? stage.topText.TranslatedString : null;
                ui.bottomText = stage.hasBottomText ? stage.bottomText.TranslatedString : null;
                ui.backText = stage.hasRepeatStage ? stage.repeatText.TranslatedString : null;
                ui.customText = stage.hasCustomStage ? stage.customText.TranslatedString : null;
                ui.continueText = stage.hasContinueStage ? stage.continueText.TranslatedString : null;
                ui.waitForParent = stage.waitForParent;

                ui.showUI();
                break;
            
            case StageType.Demo:
                nTrials.Value = stage.nTrials;
                trialRunner.interTrialInterval = stage.iti;
                if (stage.hasDemoProtocol) 
                {
                    transition.scientist.demoProtocol = stage.demoProtocol;
                    stage.demoProtocol.trialMatrix?.SetTrialMatrix();
                }
                stage.stageGameObject.SetActive(true);
                transition.transitionInThen(() => trialRunner.StartTrials());
                break;
            
            case StageType.Parent:
                ui.topText = parentsGo.TranslatedString;
                ui.cancelWaitForParent = true;
                ui.showUI();
                nTrials.Value = stage.nTrials;
                trialRunner.interTrialInterval = stage.iti;
                if (stage.hasPresetMatrix) stage.presetMatrix.SetTrialMatrix();
                stage.stageGameObject.SetActive(true);
                audioLength = audioTranslator.getLength(parentsGoAudio);
                if (parentsGoAudio != null) audioTranslator.Play(parentsGoAudio);
                this.In(audioLength).Call(trialRunner.StartTrials);
                break;
            
            case StageType.Trials:
                isParticipantsGo.boolValue = true;
                nTrials.Value = stage.nTrials;
                trialRunner.interTrialInterval = stage.iti;
                if (stage.hasPresetMatrix) stage.presetMatrix.SetTrialMatrix();
                else if (stage.hasTrialSetter) stage.trialSetter.SetTrialMatrix();
                csv.data = stage.dataHolder;
                stage.stageGameObject.SetActive(true);
                audioLength = stage.hasOpeningAudio ? audioTranslator.getLength(stage.openingAudio) : 0;
                if (stage.hasOpeningAudio) audioTranslator.Play(stage.openingAudio);
                this.In(audioLength).Call(trialRunner.StartTrials);
                break;
            
            case StageType.End:
                stage.stageGameObject.SetActive(true);
                // ui.topText = wellDone.TranslatedString;
                // ui.showUI();
                // ui.waitForParent = false;
                // this.In(2).Call(() => { ui.continueText = finish.TranslatedString; ui.showUI(); });
                break;
            
            case StageType.Custom:
                break;
        }
    }
    public void Continue()
    {
        logDataAndReset(currentStage.continueText);

        if (currentStage.stageType == StageType.End)
        {
            GoToMainMenu();
            return;
        }

        if (currentStage.hasContinueStage) nextStage = stageList.GetStage(currentStage.continueStage);
        else
        {
            var stages = stageList.stages;
            var i = stages.IndexOf(currentStage);
            nextStage = i == stages.Length - 1 ? StageList.emptyStage : stages[i+1];
        }

        if (currentStage.stageType == StageType.Demo)
        {
            transition.transitionOutThen(() => ChangeStage());
        }
        else ChangeStage();
    }

    public void Repeat()
    {
        if (currentStage.hasRepeatStage)
        {
            nextStage = stageList.GetStage(currentStage.repeatStage);
            logDataAndReset(currentStage.repeatText);
            ChangeStage();
        }
        else Debug.LogError($"Stage \"{currentStage.stageName}\" has no repeat stage. Please edit in stage list.");
    }

    public void CustomContinue()
    {
        if (currentStage.hasCustomStage)
        {
            nextStage = stageList.GetStage(currentStage.customStage);
            logDataAndReset(currentStage.customText);
            ChangeStage();
        }
        else Debug.LogError($"Stage \"{currentStage.stageName}\" has no custom stage. Please edit in stage list.");
    }

    public void QuitTask()
    {
        logDataAndReset(null);
        GoToMainMenu();
    }

    void GoToMainMenu()
    {
        csv.OutputStageData();
        SceneManager.LoadScene(7);
    }

    void OnApplicationQuit() 
    {
        LogData(null);
        csv.OutputTrialsData();
        csv.OutputStageData();
    }
    void logDataAndReset(TranslatableString buttonPressedText)
    {
        LogData(buttonPressedText);

        audioTranslator.audioSource.Stop();
        StopAllCoroutines();

        stageReset.Raise();

        isParticipantsGo.boolValue = false;
    }

    void LogData(TranslatableString buttonPressedText)
    {
        stageName.Value = currentStage.stageName;
        nViews.Value = currentStage.nViews;
        var isUi = currentStage.stageType == StageType.UI;
        uiTopText.Value = isUi && currentStage.hasTopText ? currentStage.topText.ToString() : "";
        uiBottomText.Value = isUi && currentStage.hasBottomText ? currentStage.bottomText.ToString() : "";
        uiButton1.Value = isUi && currentStage.hasRepeatStage ? currentStage.repeatText.ToString() : "";
        uiButton2.Value = isUi && currentStage.hasCustomStage ? currentStage.customText.ToString() : "";
        uiButton3.Value = isUi && currentStage.hasContinueStage ? currentStage.continueText.ToString() : "";
        buttonPressed.Value = buttonPressedText?.ToString() ?? "";
    }

    void ChangeStage()
    {
        while (nextStage.hasMaxViewings && nextStage.nViews == nextStage.maxViewings)
        {
            nextStage = stageList.GetStage(nextStage.maxViewingsStage);
        }
        if (currentStage.stageType == StageType.UI && nextStage.stageType == StageType.UI) ui.cancelWaitForParent = true;
        
        StartStage(nextStage);
    }
}