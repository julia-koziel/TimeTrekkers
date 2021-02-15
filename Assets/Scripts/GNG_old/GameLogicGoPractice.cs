using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicGoPractice : MonoBehaviour {

    public GameObject BadDog;
    public GameObject GoodDog;
    public GameObject Dogs;
    public InstructionsStateLogic logic;
    private Dog_behaviourPractice dog_Behaviour;
    private MouseClickGoPractice mouseClick;
    private MouseClickBadPractice mouseClickBad;
    private int trialNumber;
    public float velocity = 1;
    public float interval = 1.5f;
    public float time;
    public bool restart = false;
    public bool i = true;
    public bool j = true;
    private int trial = 1;
    private int practiceLevel = 1; // first practice Go-only, second mixed
    
    void Start () {
        dog_Behaviour = FindObjectOfType<Dog_behaviourPractice>();
        mouseClick = FindObjectOfType<MouseClickGoPractice>();
        mouseClickBad = FindObjectOfType<MouseClickBadPractice>();
        logic = FindObjectOfType<InstructionsStateLogic>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        time += Time.deltaTime;
       
        if (restart)
        {
            trial = 0;
            reactivate_all();
            i = true;
        }
    }

    void OnEnable() {
        // practiceLevel = getPrefsPracticeLvl(); // retrieves level from prefs, default 1
        practiceLevel=1;
        trialNumber = practiceLevel == 1 ? 5 : 10; // 5 trials for go-only, 10 for mixed
        Debug.Log("Trial number: " + trialNumber);
    }

    public void reactivate_all(){
        if (trial < trialNumber)
        {
            j = true;
            BadDog.SetActive(false);
            GoodDog.SetActive(false);
            BadDog.transform.position = new Vector3(-13, 0, 0);
            GoodDog.transform.position = new Vector3(-13, 0, 0);
            time = 0;
            trial += 1;
            pointer();
        }

        else{
            j = true;
            BadDog.SetActive(false);
            GoodDog.SetActive(false);
            BadDog.transform.position = new Vector3(-13, 0, 0);
            GoodDog.transform.position = new Vector3(-13, 0, 0);
            time = 0;
            trial = 1;
            pointer();
            logic.PostPractice();
            gameObject.SetActive(false);
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
        BadDog.SetActive(false);
        GoodDog.SetActive(false);
        BadDog.transform.position = new Vector3(-13, 0, 0);
        GoodDog.transform.position = new Vector3(-13, 0, 0);
        dog_Behaviour.reset();
        time = 0;
        trial = 1;
        // instructions.SetActive(true);
        gameObject.SetActive(false);
    }

}
