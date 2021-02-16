using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDemoLogic : MonoBehaviour
{

    public GameObject BadDino;
    public GameObject GoodDino;
    public GameObject Dogs;
    public GameObject animation;
    public GameObject[] subtitles;
    public GameObject oldtext;
    private DemoDinosBehaviour dog_Behaviour;
    private MouseClickGoPractice mouseClick;
    private MouseClickBadPractice mouseClickBad;
    private InstructionsStateLogic introLogic;
    private TheoDemoGoBehaviour Theo;
    private IEnumerator handMovement;
    private AudioSource[] aSources;
    public AudioSource dino;
    public AudioSource trex;

    public GameEvent StageEnd;
    private int trialNumber;
    public float velocity = 1;
    public float interval;
    public float time;
    public bool restart = false;
    public bool i = true;
    public bool j = true;
    public bool parentPractice;
    private int trial = 1;
    private int practiceLevel = 0; // first practice Go-only, second mixed
    private bool subtitleOn;
    private bool audioOn;

    private int[][] trials = 
    {
        new int[] {0, 0, 0, 0, 0}, // Go-only practice
        new int[] {0, 0, 1, 0, 0} // Mixed practice
    };

    private int [] cursorTrials =  {0, 1, 2, 3, 4, 5, 6, 7};


    void Start () {
        dog_Behaviour = FindObjectOfType<DemoDinosBehaviour>();
        mouseClick = FindObjectOfType<MouseClickGoPractice>();
        mouseClickBad = FindObjectOfType<MouseClickBadPractice>();
        Theo = FindObjectOfType<TheoDemoGoBehaviour>();
        introLogic = FindObjectOfType<InstructionsStateLogic>();
        aSources = GetComponents<AudioSource>();
        
        string prefsKey1 = PrefsKeys.Keys.PracticeType.ToString();
        parentPractice = PlayerPrefs.GetInt(prefsKey1, 0) == 0;
        Debug.Log("" + PlayerPrefs.GetInt(prefsKey1, 1));

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;
        animation.SetActive(false);
        oldtext.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
    {
        time += Time.deltaTime;
       
        if (restart)
        {
            trial = 0;
            reactivate_all();
            dog_Behaviour.reset();
            i = true;
        }

      for (int i = 0; i < cursorTrials.Length; i++)
            {
                if (cursorTrials[i] == trial & time>interval)
                
                 {
                    switch (i)
                    {

                        case 0:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickPip1);
                            playSound();
                            if (subtitleOn){
                                subtitles[0].SetActive(true);
                            }
                            break;

                        case 1:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickPip2);
                            if (subtitleOn){
                                subtitles[0].SetActive(true);
                            }
                            break;

                        case 2:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickPip2);
                            break;

                        case 3:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickWrong);
                             
                            break;

                        case 4:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickPip2);
                            if (subtitleOn){
                                subtitles[0].SetActive(false);
                                subtitles[1].SetActive(true);
                            }
                            break;

                        case 5:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickPip1);
                            if (subtitleOn)
                            {   
                                subtitles[0].SetActive(false);
                                subtitles[1].SetActive(false);
                            }
                            break; 

                        case 6:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickWrong);
                            if (subtitleOn)
                            {   
                                subtitles[1].SetActive(true);
                            }
                            break; 

                        case 7:
                            handMovement = moveHand(TheoDemoGoBehaviour.State.ClickPip1);
                             
                            break;                    

                    }

                    StartCoroutine(handMovement);
                }
    
    }
    }

    void OnEnable() {
        // practiceLevel = getPrefsPracticeLvl(); // retrieves level from prefs, default 1
        practiceLevel=1;
        trialNumber = practiceLevel == 0 ? 5 : 10; // 5 trials for go-only, 10 for mixed
        Debug.Log("Trial number: " + trialNumber);
    }

    public void reactivate_all(){
        if (trial < 7)
        {
            j = true;
            BadDino.SetActive(false);
            GoodDino.SetActive(false);
            BadDino.transform.position = new Vector3(-13, -100, 0);
            GoodDino.transform.position = new Vector3(-13, -100, 0);
            time = 0;
            trial += 1;
            pointer();
        }

        else{
            j = true;
            BadDino.SetActive(false);
            GoodDino.SetActive(false);
            BadDino.transform.position = new Vector3(-13, -100, 0);
            GoodDino.transform.position = new Vector3(-13, -100, 0);
            time = 0;
            trial = 1;
            pointer();
            gameObject.SetActive(false);
            subtitles[1].SetActive(false);
            StageEnd.Raise();
        }
        
    }

    public void pointer()
    { //on/off system so that it is ran once.
        i = !i;
    }

    public void pointer2()
    { //on/off system so that it is ran once.
        j = !j;
    }

    public void timetozero(){
        time = 0;
    }

    public int getPracticeLevel(){
        return practiceLevel;
    }

    private int getPrefsPracticeLvl(){
        string practiceLvlKey = PrefsKeys.Keys.GNG_PracticeLevel.ToString();
        return PlayerPrefs.GetInt(practiceLvlKey, 1); // default level = 1
    }

    public void setPrefsPracticeLvl(int level){
        string practiceLvlKey = PrefsKeys.Keys.GNG_PracticeLevel.ToString();
        PlayerPrefs.SetInt(practiceLvlKey, level);
    }

    public int getTrial()
    {
        return trial;
    }

    public void skip()
    {
        i = true;
        j = true;
        BadDino.SetActive(false);
        GoodDino.SetActive(false);
        BadDino.transform.position = new Vector3(-13, -100, 0);
        GoodDino.transform.position = new Vector3(-13, -100, 0);
        dog_Behaviour.reset();
        time = 0;
        trial = 1;
        introLogic.moveToNextState();
        gameObject.SetActive(false);
    }

     private IEnumerator moveHand(TheoDemoGoBehaviour.State direction)
    {
        float delay = direction == TheoDemoGoBehaviour.State.ClickPipLate ? 0.8f : 0.6f;
        yield return new WaitForSeconds(delay);
        Theo.move(direction);
    }

    public void playTrex()
    {
        trex.Play();
    }

    public void playSound()
    {
        dino.Play();
    }

}
