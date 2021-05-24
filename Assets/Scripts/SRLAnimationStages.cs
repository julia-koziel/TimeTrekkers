using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRLAnimationStages : MonoBehaviour
{
    private AudioSource[] aSources;
    public GameObject introscreen;
    public GameObject[] textboxes;
    public GameObject farmer;
    public GameObject farmer2;
    public GameObject donkey;
    public GameObject Theo;
    public GameObject repeatButton;
    public GameObject continueButton;
    public GameObject demo;
    public GameObject animation;
    public GameObject textboxes1;
    public GameObject map;

    public bool walk;
    public bool audioOn;
    public bool subtitleOn;
    public float time;
    public int velocity = 2; 
    public float x;
    private float s;
    private float t;

    private State stateToLoad = State.None;
    private State currentState = State.None;

    private Animator animator;

    private enum State {
        None,
        FarmerWalk, 
        ZoomIn, 
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
        stateToLoad = State.FarmerWalk;
        animator = GetComponent<Animator>();
        aSources = GetComponents<AudioSource>();
        textboxes1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        AudioBundle[] audioInfo; 
        switch (stateToLoad)
    {
        case State.FarmerWalk:
        introscreen.SetActive(false);
        farmer.SetActive(true);
        map.SetActive(false);
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0),
                };
        walk = true;

        if (subtitleOn)
        {
            textboxes[0].SetActive(true);
        }
        finishStateLoad();
        StartCoroutine(playAudio(audioInfo));

        break;

        case State.ZoomIn:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 0),
                };
        // Theo.SetActive(true);
        animator.SetTrigger("zoom");
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();

        if (subtitleOn)
        {
            textboxes[0].SetActive(false);
            textboxes[1].SetActive(true);
        }
        break;

        case State.Theo:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 0f),
                };

        Theo.SetActive(true);
        farmer.SetActive(false);
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();
        if (subtitleOn)
        {
            textboxes[1].SetActive(false);
            textboxes[2].SetActive(true);
        }

        break;

        case State.Demo:

            textboxes[2].SetActive(false);
            gameObject.SetActive(false);
            map.SetActive(false);
            animation.SetActive(false);
            demo.SetActive(true);

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
    
    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            yield return new WaitForSeconds(audioInfo[i].delay);
            currentAudio = aSources[audioInfo[i].index];
            if (audioOn) {currentAudio.Play();}
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }
    

        moveToNextState();

    }

    private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(3f);
        moveToNextState();
    }

    public void WatchAgain()
    {
        stateToLoad = State.FarmerWalk;
        repeatButton.SetActive(false);
        continueButton.SetActive(false); 
    }

    public void StartDemo()
    {
        gameObject.SetActive(false);
        demo.SetActive(true);
    }
}
