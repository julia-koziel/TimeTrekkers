using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAttackAnimation : MonoBehaviour
{
    private AudioSource[] aSources;
    public GameObject[] textboxes;

    public GameEvent StageEnd;
    public GameObject[] ships;
    public GameObject target;
    public GameObject Theo;
    public GameObject repeatButton;
    public GameObject continueButton;
    public GameObject demo;
    public GameObject intro;
    public GameObject background;

    public bool audioOn;
    public bool subtitleOn;
    
    public bool move;

    public float time;
    public float velocity = 2f; 
    public float x;
    private float s;
    private float t;

     private enum State {
        None,
        TheoIntro, 
        PirateShip,  
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
        intro.SetActive(false);
        
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
        foreach (GameObject ship in ships)
        {
            ship.SetActive(true);
        }

        if (subtitleOn)
        {
            textboxes[0].SetActive(true);
        }
        
        StartCoroutine(playAudio(audioInfo));

        finishStateLoad();
        break;

        case State.PirateShip:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 1f),};
        target.SetActive(true);
        StartCoroutine(playAudio(audioInfo));
        if (subtitleOn)
        {
            textboxes[0].SetActive(false);
            textboxes[1].SetActive(true);
        }

        finishStateLoad();
        break;

        case State.Shipsleave:

        if (subtitleOn)
        {
            textboxes[1].SetActive(false);
            textboxes[2].SetActive(true);
        }

        move = true;
        finishStateLoad();
        StartCoroutine(clickdelayshort());
        break;


        case State.Demo:
        foreach (GameObject textbox in textboxes)
        {
            textbox.SetActive(false);
        }
        StageEnd.Raise();
        gameObject.SetActive(false);
        
        finishStateLoad();
        break;

    }

        if(move)
        {
            time += Time.deltaTime;
            x = 2 * time * velocity;
            s = 1 * time * velocity;
            t = 3 * time * velocity;
            ships[0].transform.position = new Vector3(x,1,0);
            ships[1].transform.position = new Vector3(s,-1,0);
            ships[2].transform.position = new Vector3(t,-2,0);

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
