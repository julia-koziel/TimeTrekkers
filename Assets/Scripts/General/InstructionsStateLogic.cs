using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsStateLogic : MonoBehaviour
{ 
    private AudioSource[] aSources;      
    public int practicelevel;
    public GameObject demo;
    public GameObject instructions;
    public GameObject practice;
    public GameObject ParentPractice;
    public GameObject end;

    public GameObject text1;
    public GameObject text2;
    public GameObject repeatbutton;
    public GameObject parentPracticebutton;
    public GameObject parentPretestbutton;
    public GameObject childGo;
    public GameObject practicebutton;
    public GameObject startpracticebutton;
    public GameObject starttrials;
    public GameObject trials;
    public GameObject preTest;
    public GameObject continuebutton;

    public bool democlicked;
    public bool practiceclicked = false;
    public bool parentPractice;
    public bool parentPretest;
    private int pretestFails=0;
    private State stateToLoad = State.None;
    private State currentState = State.None;
    private CategorisationPretest pretest;
    


    public enum State {
        PostDemoParent, 
        PostDemoChild,
        ParentPractice,
        PostParentPractice,
        Pretest, 
        PretestCorrect,
        PostPretest,
        parentPretestbutton, 
        PretestParent,
        PretestIncorrect,
        Practice,
        PostPractice,
        TrialsButtonPressed,
        Trials,
        None,
    }

    
    void Awake()
    {
        string prefsKey = PrefsKeys.Keys.PracticeType.ToString();
        parentPractice = PlayerPrefs.GetInt(prefsKey, 0) == 0;
        parentPractice = false;
        parentPretest = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        aSources = GetComponents<AudioSource>();  
       pretest = FindObjectOfType<CategorisationPretest>(); 
       
       if (parentPractice)
       {
           stateToLoad = State.PostDemoParent;
       }

       else
       {
           stateToLoad = State.PostDemoChild;
       }
       
    }

    // Update is called once per frame
    void Update()
    {
         AudioBundle[] audioInfo;
    
          switch (stateToLoad)
        {

            case State.PostDemoParent:

            text1.SetActive(true);
            parentPracticebutton.SetActive(true);
            childGo.SetActive(true);
            
            break;

            case State.PostDemoChild:

            repeatbutton.SetActive(true);
            continuebutton.SetActive(true);

            break;

            case State.ParentPractice:

            text1.SetActive(false);
            parentPracticebutton.SetActive(false);
            childGo.SetActive(false);
            ParentPractice.SetActive(true);
            parentPractice = true;

            break;

            case State.PostParentPractice:
            continuebutton.SetActive(true);
            parentPractice = false;
            parentPretest = false;


            break;

            
            case State.Pretest:
               audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0),};
       ;
            text1.SetActive(false);
            repeatbutton.SetActive(false);
            parentPracticebutton.SetActive(false);
            continuebutton.SetActive(false);
            childGo.SetActive(false);
            preTest.SetActive(true);
            parentPretest = false;
            parentPractice = false;

            break;
            
            case State.PostPretest:
            preTest.SetActive(false);
            break;


            case State.PretestCorrect:
            practicebutton.SetActive(true);

            break;

            case State.Practice:
               audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 0),};
       ;
            practicebutton.SetActive(false);
            text1.SetActive(false);
            parentPracticebutton.SetActive(false);
            childGo.SetActive(false);

            StartCoroutine(playAudio(audioInfo));

            break;

            case State.parentPretestbutton:
            text2.SetActive(true);
            parentPretestbutton.SetActive(true);
            continuebutton.SetActive(false);
            break;

            case State.PretestParent:
            text2.SetActive(false);
            parentPretestbutton.SetActive(false);
            preTest.SetActive(true);

            parentPretest=true;

            break;
            
            case State.PostPractice:
            repeatbutton.SetActive(true);
            starttrials.SetActive(true);

            break;

            case State.TrialsButtonPressed:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay: 0),};
       ;
            trials.SetActive(true);
            gameObject.SetActive(false);
            StartCoroutine(playAudio(audioInfo));
            break;

            case State.Trials:
            trials.SetActive(true);
            gameObject.SetActive(false);
            break;

            case State.None:
            
            break;

        }
  
    }


     public void moveToNextState()
    {
        stateToLoad = currentState.Next();
    }

    public void repeatbuttonPrevious()
    {
        if (practiceclicked)
        {
            StartPractice();
        }

        else 
        {
            demo.SetActive(true);
            instructions.SetActive(false);
            repeatbutton.SetActive(false);
            parentPracticebutton.SetActive(false);
            continuebutton.SetActive(false);
        }
    }

    public void StartParentPractice()
    {
        stateToLoad = State.ParentPractice;
        text1.SetActive(false);
        parentPracticebutton.SetActive(false);
        childGo.SetActive(false);
    }

    public void StartPractice()
    {
        stateToLoad = State.Practice;
        practiceclicked = true;
        practice.SetActive(true);
        practicebutton.SetActive(false);
        repeatbutton.SetActive(false);
        starttrials.SetActive(false);
    }

    public void StartPretest()
    {   
        stateToLoad = State.Pretest;
        repeatbutton.SetActive(false);
        continuebutton.SetActive(false);
        text2.SetActive(false);
        parentPretestbutton.SetActive(false);
    }

    public void PostPractice()
    {
        if (parentPractice)
        {   
            stateToLoad = State.PostParentPractice;
            parentPractice = false;
        }
        else
        {
        stateToLoad = State.PostPractice;
        }
    }
    public void StartTrials()
    {
        stateToLoad = State.TrialsButtonPressed;
    }

    public void PretestCorrect()
    {
        if (parentPretest)
        {
           continuebutton.SetActive(true);
           preTest.SetActive(false);
        }
        
        else
        {
        stateToLoad = State.PretestCorrect;
        preTest.SetActive(false);
        }
    }

    public void PretestIncorrect()
    {
        if (parentPractice)
        {
            stateToLoad = State.parentPretestbutton;
        }

        else
        {
           StartPretest();
        }

        pretestFails++;

        if(pretestFails>2)
        {
            end.SetActive(true);
            gameObject.SetActive(false);
        }

    }

    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            currentAudio.Play();
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }
    }

}

