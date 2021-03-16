using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StardustAnimation : MonoBehaviour
{
    public GameObject Theo;
    public GameObject[] textboxes;
    private TheoDemoRewardSocialBehaviour TheoBehaviour;

    public GameEvent StageEnd;

    public GameObject cockpit;
    private Vector3[] positions;

    private Animator animator;
    private AudioSource[] aSources;
    private bool audioOn;
    private bool subtitleOn;

    private enum State {
        None,
        Intro,
        Zoom,

        End, 
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        TheoBehaviour = Theo.GetComponent<TheoDemoRewardSocialBehaviour>();
        aSources = GetComponents<AudioSource>();

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;

        stateToLoad = State.Intro;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;
        
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {
            case State.Intro:
                Theo.SetActive(true);
                cockpit.SetActive(true);
                finishStateLoad();

                 audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f),
                };
                StartCoroutine(playAudio(audioInfo));
                if (subtitleOn)
                {
                    textboxes[0].SetActive(true);
                }
                break;

            case State.Zoom:
                
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 1),
                };
                if (subtitleOn)
                {
                    textboxes[0].SetActive(true);
                }
                animator.SetTrigger("zoom");
                finishStateLoad();
    
                break;

            case State.End:
                textboxes[0].SetActive(false);
                break;
                
            case State.None:
                break;

            default:
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
            currentAudio = aSources[audioInfo[i].index];
            if (audioOn) { currentAudio.Play();
            Debug.Log("audio"); }
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        moveToNextState();
    }

    private IEnumerator clickRoutine()
    {
        yield return new WaitForSeconds(2.5f);

        moveToNextState();
    }
    
    public void TheoClicked()
    {
        StageEnd.Raise();
        gameObject.SetActive(false);
        textboxes[0].SetActive(false);
    }
}
