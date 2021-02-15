using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RLAnimationStages : MonoBehaviour
{       
    public GameObject crab1;
    public GameObject crab2;
    public GameObject Theo;
    public GameObject map;
    public float time; 
    public bool audioOn;
    public bool subtitleOn;
    public GameObject[] textboxes;
    private State stateToLoad = State.None;
    private State currentState = State.None;
    private AudioSource[] aSources;
    public GameEvent stageEnd;

    public enum State{
        Intro,
        Crab1, 
        Crab2,
        Theo,
        Theo2, 
        Demo,
        None,
    }
        
    // Start is called before the first frame update
    void Start()
    {
        aSources = GetComponents<AudioSource>();

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;
        crab1.SetActive(true);
        crab2.SetActive(false);
        stateToLoad = State.Intro;
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;
       time += Time.deltaTime;

       if (time>1)
       {
           crab1.SetActive(false);
           crab2.SetActive(true);
       }

        switch (stateToLoad)
        {
            case State.Intro:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f),
                };
            // StartCoroutine(playAudio(audioInfo));
            StartCoroutine(clickRoutine());
            finishStateLoad();
            map.SetActive(true);
            break;

            case State.Crab1:
            crab1.SetActive(false);
            crab2.SetActive(true);
            map.SetActive(false);
            StartCoroutine(clickRoutine());
            finishStateLoad();

            break;

            case State.Crab2:
            Theo.SetActive(true);

            StartCoroutine(clickRoutine());
            finishStateLoad();

            break;

            case State.Theo:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f)
                };
            if (subtitleOn)
            {
                textboxes[0].SetActive(true);
            }
            StartCoroutine(playAudio(audioInfo));
            map.SetActive(false);
            finishStateLoad();

            break;

            case State.Theo2:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: -0.5f)
                };
            if (subtitleOn)
            {
                textboxes[0].SetActive(false);
                textboxes[1].SetActive(true);
            }
            
            StartCoroutine(playAudio(audioInfo));
            finishStateLoad();

            break;

            case State.Demo:

            gameObject.SetActive(false);
            stageEnd.Raise();

            finishStateLoad();

            break;

            case State.None:

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
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            yield return new WaitForSeconds(audioInfo[i].delay);

            if (audioOn) {currentAudio.Play();}
            yield return new WaitForSeconds(currentAudio.clip.length);
            
            moveToNextState();
        }
    

 
    }

    private IEnumerator clickRoutine()
    {
       
        yield return new WaitForSeconds(3f);

        moveToNextState();
    }


}
