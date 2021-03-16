using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLAnimation : MonoBehaviour
{   
    private AudioSource[] aSources;
    public GameObject introscreen;
    public GameObject[] textboxes;
    public GameObject[] ships;
    public GameObject storm;
    public GameObject Theo;
    public GameObject demo;
    public GameObject background;
    public GameObject introscreenback;
    private ShipsBehaviourAnimation s;

    public bool audioOn;
    public bool subtitleOn;
    
    public bool move;

    public float time;
    public float velocity = 0.1f; 
    public float x;

     private enum State {
        None,
        TheoIntro, 
        Storm,  
        Shipsleave, 
        Demo,
        Buttons,
        }

    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;
        
        stateToLoad = State.TheoIntro; 
        aSources = GetComponents<AudioSource>();  
        s = FindObjectOfType<ShipsBehaviourAnimation>();
        background.SetActive(true);
        introscreenback.SetActive(false);
        introscreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        AudioBundle[] audioInfo; 
        switch (stateToLoad)
    {
        case State.TheoIntro:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 1.5f),};
        Theo.SetActive(true);

        if (subtitleOn)
        {
            textboxes[0].SetActive(true);
        }
        
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();
        break;

        case State.Storm:
        Debug.Log("storm");
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 1f),};
        StartCoroutine(playAudio(audioInfo));
        storm.SetActive(true);
        if (subtitleOn)
        {
            textboxes[0].SetActive(false);
            textboxes[1].SetActive(true);
        }

        finishStateLoad();
        break;

        case State.Shipsleave:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 0.5f),};

        if (subtitleOn)
        {
            textboxes[1].SetActive(false);
            textboxes[2].SetActive(true);
        }

       
        finishStateLoad();
        StartCoroutine(playAudio(audioInfo));
        break;


        case State.Demo:
        demo.SetActive(true);
        gameObject.SetActive(false);
        
        finishStateLoad();
        break;

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

   private IEnumerator clickdelayshort()
    {
       yield return new WaitForSeconds(3f);
        moveToNextState();
    }  

    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            yield return new WaitForSeconds(audioInfo[i].delay);

            if (audioOn) {currentAudio.Play();}
            yield return new WaitForSeconds(currentAudio.clip.length);

            moveToNextState();
        }
        
    }

}
