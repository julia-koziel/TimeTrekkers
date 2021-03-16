using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLogicStardust : MonoBehaviour
{
    public GameObject star;
    public GameObject[] subtitles;
    public GameEvent StageEnd;
    private DemoStarBehaviour star_Behaviour;
    private MouseClickGoPractice mouseClick;
    private MouseClickBadPractice mouseClickBad;
    private TheoDemoStardustBehaviour Theo;

    public GameObject theo;
    public GameObject Theo2;
    private IEnumerator handMovement;
    private AudioSource[] aSources;
    public AudioSource sparkle;
    public GameObject[] stardust;
    public GameObject demo;
    public GameObject[] barfill;
    public AudioSource trex;
    private int trialNumber;
    public float velocity = 12;
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

    public GameObject startButton;

    private int[][] trials = 
    {
        new int[] {0, 0, 0, 0}, // Go-only practice
        new int[] {0, 0, 0, 0, 0} // Mixed practice
    };

    private int [] cursorTrials =  {0, 1, 2, 3, 4, 5};


    void Start () {
        star_Behaviour = FindObjectOfType<DemoStarBehaviour>();
        mouseClick = FindObjectOfType<MouseClickGoPractice>();
        mouseClickBad = FindObjectOfType<MouseClickBadPractice>();
        Theo = FindObjectOfType<TheoDemoStardustBehaviour>();
        aSources = GetComponents<AudioSource>();

        string prefsKey1 = PrefsKeys.Keys.PracticeType.ToString();
        parentPractice = PlayerPrefs.GetInt(prefsKey1, 0) == 0;
        Debug.Log("" + PlayerPrefs.GetInt(prefsKey1, 1));

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;

        if (audioOn)
        {
            aSources[1].Play();
        }
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        time += Time.deltaTime;
        if(time>7)
            {
            startButton.SetActive(true);
            }
        if (restart)
        {
            trial = 0;
            reactivate_all();
            star_Behaviour.reset();
            i = true;
        }

      for (int i = 0; i < cursorTrials.Length; i++)
            {
                if (cursorTrials[i] == trial & time>0)
                
                 {
                    switch (i)
                    {

                        case 0:
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickPip2);
                            playSound();
                            if (subtitleOn){
                                subtitles[0].SetActive(true);
                            }
                            break;

                        case 1:
                            velocity=25;
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickPip2);
                            if (subtitleOn){
                                subtitles[0].SetActive(true);
                            }
                        
                            break;

                        case 2:
                            velocity=15;
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickPip2);
                            break;

                        case 3:
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickPip2);
                            if (subtitleOn){
                                subtitles[0].SetActive(false);
                                subtitles[1].SetActive(true);
                            }
                            break;

                        case 4:
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickWrong);
                            if (subtitleOn){
                                subtitles[0].SetActive(false);
                                subtitles[1].SetActive(true);
                            }
                            if (audioOn)
                            {
                                aSources[2].Play();
                            }
                            Debug.Log("wrong");
                            break;

                        case 5:
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickWrong);
                            
                            if (subtitleOn)
                            {   
                                subtitles[0].SetActive(false);
                                subtitles[1].SetActive(false);
                            }
                            break; 

                        case 6:
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickWrong);
                            if (subtitleOn)
                            {   
                                subtitles[1].SetActive(true);
                            }
                            break; 

                        case 7:
                            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickPip1);
                             
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
        if (trial < 3)
        {
            j = true;
        
            star.SetActive(true);
            star.transform.position = new Vector3(-20, 0, 0);
            time = 0;
            trial += 1;
            pointer();
        }

        else{
            j = true;
            
            star.SetActive(false);
            star.transform.position = new Vector3(-10, 0, 0);
            time = 0;
            // trial = 1;
            pointer();
            handMovement = moveHand(TheoDemoStardustBehaviour.State.ClickWrong);
            if (audioOn)
            {
                aSources[2].Play();
            }

            

            theo.SetActive(false);
            Theo2.SetActive(true);
            // StageEnd.Raise();
            
            StartCoroutine(handMovement);
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
       demo.SetActive(false);
       StageEnd.Raise();
    }

     private IEnumerator moveHand(TheoDemoStardustBehaviour.State direction)
    {
        float delay = direction == TheoDemoStardustBehaviour.State.ClickPipLate ? 0.1f : 0.1f;
        yield return new WaitForSeconds(delay);
        Theo.move(direction);
    }


    public void playSound()
    {
        sparkle.Play();
        stardust[trial-1].SetActive(true);
        barfill[trial-1].SetActive(true);
    }

}
