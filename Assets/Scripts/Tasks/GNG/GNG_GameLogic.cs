using UnityEngine;
using UnityEngine.SceneManagement;
using Appccelerate.StateMachine.Machine;
using Appccelerate.StateMachine;

public class GNG_GameLogic : MonoBehaviour
{
    public UILogic ui;
    public CameraTransition transition;
    public TrialRunner trialRunner;
    public CsvReadWrite csv;
    [Space(10)]
    public GameEvent stageRepeat;
    [Space(10)]
    public GameObject introAnimation;
    public GameObject stimuliPresenter;
    public GNG_PretestStimuliManager pretest;
    public DataHolder pretestData;
    public GNG_StimuliManager main;
    public DataHolder mainData;
    public GameObject endPuppy;
    [Space(10)]
    public IntVariable nRepeats;
    public IntVariable nTrials;
    public BoolVariable parentsGo;

    #region State Machine
    enum States
    {
        // Add your own states here
        Animation, AnimationUI,
        StimuliUI, Stimuli,
        PretestDemo, PretestParent, Pretest, 
        DemoUI, Demo, 
        ParentPracticeUI, ParentPractice,
        PracticeUI, Practice, 
        MainUI, Main,
        End
    }
    enum Events
    {
        Continue, Repeat, CustomContinue
    }
    PassiveStateMachine<States, Events> machine;
    #endregion
    void Awake()
    {
        nRepeats.Value = 0;

        var builder = new StateMachineDefinitionBuilder<States, Events>();

        builder.In(States.Animation)
                .OnEntry(() => introAnimation.SetActive(true))
                .On(Events.Continue)
                    .If(() => ++nRepeats < 3).Goto(States.AnimationUI)
                    .Otherwise().Goto(States.StimuliUI);
        
        builder.In(States.AnimationUI)
                .OnEntry(() => ui.showUI("Did your child watch the video?", "Yes", "No"))
                .On(Events.Repeat).Goto(States.Animation)
                .On(Events.Continue).Goto(States.StimuliUI)
                .Execute(() => nRepeats.Value = 0);

        builder.In(States.StimuliUI)
                .OnEntry(() => ui.showButtons("Continue"))
                .On(Events.Continue).Goto(States.Stimuli);
        
        builder.In(States.Stimuli)
                .OnEntry(() => {
                    stimuliPresenter.SetActive(true);
                })
                .On(Events.Continue).Goto(States.PretestDemo);
        
        builder.In(States.PretestDemo)
                .OnEntry(() => {
                    nTrials.Value = 2;
                    pretest.SetActive(true);
                    transition.transitionInThen(() => trialRunner.StartTrials());
                })
                .On(Events.Continue).Goto(States.PretestParent);
                
        builder.In(States.PretestParent)
                .OnEntry(() => {
                    nTrials.Value = 2;
                    parentsGo.boolValue = true;
                    pretest.SetActive(true);
                    ui.showUI("Parent's Go", textAtBottom: true);
                    trialRunner.StartTrials();
                })
                .OnExit(() => parentsGo.boolValue = false)
                .On(Events.Continue).Goto(States.Pretest);

        builder.In(States.Pretest)
                .OnEntry(() => {
                    nTrials.Value = 2;
                    csv.data = pretestData;
                    ui.showUI("Child's Go: \"Touch the Puppy!\"", textAtBottom: true);
                    pretest.SetActive(true);
                })
                .On(Events.Continue).Goto(States.DemoUI);

        builder.In(States.DemoUI)
                .OnEntry(() => ui.showButtons("Continue"))
                .On(Events.Continue).Goto(States.Demo);
        
        builder.In(States.Demo)
                .OnEntry(() => {
                    nTrials.Value = 5;
                    main.SetActive(true);
                    transition.transitionInThen(() => trialRunner.StartTrials());
                })
                .On(Events.Continue).Goto(States.ParentPracticeUI);
        
        builder.In(States.ParentPracticeUI)
                .OnEntry(() => ui.showUI("Choose \"Parent Practice\" if your child hasn't fully understood",
                                "Practice", customText: "Parent Practice"))
                .On(Events.Continue)
                    .If(() => parentsGo).Goto(States.ParentPractice)
                    .Otherwise().Goto(States.PracticeUI);
        
        builder.In(States.ParentPractice)
                .OnEntry(() => {
                    nTrials.Value = 10;
                    ui.showUI("Parent's Go", textAtBottom: true);
                    main.SetActive(true);
                    trialRunner.StartTrials();
                })
                .OnExit(() => parentsGo.boolValue = false)
                .On(Events.Continue).Goto(States.PracticeUI);
        
        builder.In(States.PracticeUI)
                .OnEntry(() => ui.showUI("Catch the Puppy,\ndon’t touch the angry kitty!", "Child's Go"))
                .On(Events.Continue).Goto(States.Practice);
        
        builder.In(States.Practice)
                .OnEntry(() => {
                    nTrials.Value = 10;
                    csv.data = mainData;
                    main.SetActive(true);
                    trialRunner.StartTrials();
                })
                .On(Events.Continue).Goto(States.MainUI);
        
        builder.In(States.MainUI)
                .OnEntry(() => ui.showUI("Choose repeat if your child wasn’t paying attention", "Start", "Repeat"))
                .On(Events.Repeat).Goto(States.Practice)
                .On(Events.Continue).Goto(States.Main);
        
        builder.In(States.Main)
                .OnEntry(() => {
                    nTrials.Value = 40;
                    csv.data = mainData;
                    main.SetActive(true);
                    trialRunner.StartTrials();
                })
                .On(Events.Continue).Goto(States.End);
        
        builder.In(States.End)
                .OnEntry(() => {
                    endPuppy.SetActive(true);
                    ui.showUI("Well done!", waitForParent: false);
                    this.In(2).Call(() => ui.showUI("Well done!", "Finish", waitForParent: false));
                })
                .On(Events.Continue).Execute(() => SceneManager.LoadScene(0));
        
        builder.WithInitialState(States.Animation);

        machine = builder.Build().CreatePassiveStateMachine();
    }

    void Start()
    {
        machine.Start();
    }
    public void Continue()
    {
        if (transition.transitionCalled) transition.transitionOutThen(() => machine.Fire(Events.Continue));
        else machine.Fire(Events.Continue);
    }

    public void Repeat()
    {
        machine.Fire(Events.Repeat);
    }

    public void CustomContinue()
    {
        machine.Fire(Events.CustomContinue);
    }
}