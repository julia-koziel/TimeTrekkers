using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBirdLogic : MonoBehaviour
{
    private AudioSource[] aSources;

    public GameObject Theo;
    public GameObject Theo2;
    public GameObject bird1;
    public GameObject bird2;
    public GameObject bird3;
    public GameObject singing1;
    public GameObject singing2;
    public GameObject singing3;


    public GameObject lowpitch;
    public GameObject highpitch;
    public GameObject WatchAgain;
    public GameObject StartDemo;
    public GameObject demo;

    public bool stable =false;
    public bool flyout = false;

    private State stateToLoad = State.None;
    private State currentState = State.None;

    private enum State {
        None,
        TheoIntro,
        listen1,
        bird1,
        ITI,
        bird2,
        birdsFlying1,
        bird3,
        birdsFlying2,
        Buttons,}
    // Start is called before the first frame update
    void Start()
    {
        stateToLoad = State.TheoIntro; 
        aSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;

        switch(stateToLoad)
        {

            case State.TheoIntro:

            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f),};
                    Theo.SetActive(true);
            StartCoroutine(playAudio(audioInfo));
            bird1.SetActive(true);
            bird2.SetActive(true);

            finishStateLoad();

            break;

            case State.listen1:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 1.2f),};
            StartCoroutine(playAudio(audioInfo));
           
            finishStateLoad();
            break;


            case State.bird1:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 1f),};

            singing1.SetActive(true);
            highpitch.SetActive(true);
            StartCoroutine(playAudio(audioInfo));
            
            finishStateLoad();


            break;

            case State.ITI:
            Theo.SetActive(true);
            Theo2.SetActive(false);
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay: 0.5f),};
            flyout = true;
            singing2.SetActive(true);
            lowpitch.SetActive(true);
            highpitch.SetActive(false);
            singing1.SetActive(false);

            StartCoroutine(clickdelay());
            finishStateLoad();

            break;

            case State.bird2:
            Theo.SetActive(false);
            Theo2.SetActive(true);
            bird2.SetActive(true);
            singing2.SetActive(false);
            lowpitch.SetActive(false);
            StartCoroutine(clickdelay());
            finishStateLoad();


            break;

            case State.birdsFlying1:
            Theo.SetActive(true);
            Theo2.SetActive(false);
                
            audioInfo = new AudioBundle[] 
            {
                    new AudioBundle(index: 3, delay: 0.1f),
            };
            bird3.SetActive(true);
            StartCoroutine(playAudio(audioInfo));

            finishStateLoad();

            break;

            case State.bird3:
        
            bird3.SetActive(true);
            singing3.SetActive(true);
            highpitch.SetActive(true);

            StartCoroutine(clickdelay());
            finishStateLoad();

            break;

            case State.birdsFlying2:
            singing3.SetActive(false);
            stable=true;
            StartCoroutine(clickdelay());
            finishStateLoad();

            break;

            case State.Buttons:

            WatchAgain.SetActive(true);
            StartDemo.SetActive(true);
            bird1.SetActive(false);
            bird2.SetActive(false);
            bird3.SetActive(false);
            Theo.SetActive(false);
            Theo2.SetActive(true);
        
            break;
        }
        
    }

     void moveToNextState()
    {
        // Function may not be needed depending on audio files
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
            currentAudio.Play();
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        moveToNextState();
    }

    private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1.5f);
        moveToNextState();
    }

    public void watchAgain()
    {
        stateToLoad = State.TheoIntro;
    }

    public void startDemo()
    {
        demo.SetActive(true);
        gameObject.SetActive(false);
    }
}
