using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoAnimation : MonoBehaviour
{
    private AudioSource[] aSources;
    public GameObject[] textboxes;
    public GameObject intro;
    public GameObject herb1;
    public GameObject herb2;
    public GameObject herb3;
    public GameObject pteresa;
    public GameObject tRex;
    public GameObject Theo;
    public GameObject button1;
    public GameObject button2;
    public GameObject repeatButton;
    public GameObject continueButton;
    public GameObject demo;
    public GameEvent StageEnd;
    public GameObject background;
    

    public bool audioOn;
    public bool subtitleOn;
    
    public bool Trun = false;
    public bool Theorun = false;
    public bool Herbrun = false;
    public float time;
    public int velocity; 
    public float x;
    private float s;
    private float t;

    private State stateToLoad = State.None;
    private State currentState = State.None;


    private enum State {
        None,
        PteresaIntro, 
        TrexRun,   
        PteresaSad,
        PteresaEnd, 
        Theo,
        Demo,
        }

    // Start is called before the first frame update
    void Start()
    {
        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;
        
        stateToLoad = State.PteresaIntro; 
        aSources = GetComponents<AudioSource>();  
        button1.SetActive(false);
        button2.SetActive(false);
        intro.SetActive(false);
        background.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        AudioBundle[] audioInfo; 
        switch (stateToLoad)
    {
        case State.PteresaIntro:
        
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: -0.5f),};
       ;

        StartCoroutine(playAudio(audioInfo));
        if (subtitleOn)
        {
            textboxes[0].SetActive(true);
        }
        finishStateLoad();
        
        herb1.transform.position = new Vector3(6.5f, 0, 0);
        herb2.transform.position = new Vector3(4, -2.5f, 0);
        herb3.transform.position = new Vector3(-3, -1, 0);
        herb1.SetActive(true);
        herb2.SetActive(true);
        herb3.SetActive(true);
        Theo.SetActive(false);

        break;

        case State.TrexRun:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: -1f),};
       ;
        tRex.SetActive(true);
        
        if (subtitleOn)
        {
           textboxes[1].SetActive(true);
           textboxes[0].SetActive(false);
        }
        
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();

        break;


        case State.PteresaSad:

        herb1.SetActive(false);
        herb2.SetActive(false);
        herb3.SetActive(false);
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: -0.5f),};
       ;
        StartCoroutine(playAudio(audioInfo));
        if (subtitleOn)
        {
            textboxes[2].SetActive(true);
            textboxes[1].SetActive(false);
            textboxes[0].SetActive(false);
        }
    
        finishStateLoad();

        break;

        case State.PteresaEnd:

        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay: 1f),};
       ;
        if (subtitleOn)
        {
            textboxes[3].SetActive(true);
            textboxes[2].SetActive(false);
        }
        
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();

        break;

        case State.Theo:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 4, delay: 2),};
        Theo.SetActive(true);
        Theorun = true;
       ;
        if (subtitleOn)
        {
            textboxes[4].SetActive(true);
            textboxes[3].SetActive(false);
        }
        
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();

        break;


        case State.Demo:

        Herbrun = false;
        herb1.SetActive(false);
        herb2.SetActive(false);
        herb3.SetActive(false);
        tRex.SetActive(false);
        textboxes[4].SetActive(false);
        StageEnd.Raise();
        gameObject.SetActive(false);

        break;
    }

        if(Herbrun)
        {
            time += Time.deltaTime;
            x = 1.2f * time * velocity;
            s = 1 * time * velocity;
            t = 1 * time * velocity;
            herb1.transform.position = new Vector3(x,-0,0);
            herb1.transform.Rotate(0, 0, 0, Space.World);
            herb2.transform.position = new Vector3(s,-2,0);
            herb3.transform.position = new Vector3(-t,-3,0);

        }

    }

    public void moveToNextState()
    {
        stateToLoad = currentState.Next();
    }

    void finishStateLoad()
    {
        currentState = stateToLoad;
        stateToLoad = State.None;
    }
    
    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            if (audioOn) {currentAudio.Play();}
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        moveToNextState();

    }
    // private IEnumerator displaySubtitle(GameObject textboxes[i])
    // {
    // }


    public void WatchAgain()
    {
        stateToLoad = State.PteresaIntro;
        repeatButton.SetActive(false);
        continueButton.SetActive(false); 
        Herbrun = false;
    }

    public void StartDemo()
    {
        gameObject.SetActive(false);
        StageEnd.Raise();
    }



}
