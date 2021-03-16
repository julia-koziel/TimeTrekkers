using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class MainMenuLogicNew : MonoBehaviour
{
    private AudioSource[] aSources;
    private MachineOnClick machineClick;
    private IDScript id;

    public GameObject titlescreen;
    public GameObject consent;
    public GameObject intro;
    public GameObject introscreen;
    public GameObject menuscreen;
    public GameObject console;
    public GameObject machine;
    private Animator animator;
    public GameObject button;
    public GameObject text;
    public GameObject AudioToggle;
    public GameObject SubtitleToggle;
    public GameObject ParentToggle;

    public string participantId;
    Queue<string> files;

    public bool input;

    public bool first = true;
    public bool wasClicked;
    public float time;
    private bool introLoaded = false;

    private State stateToLoad = State.None;
    private State currentState = State.None;

    private enum State{ titleScreen, 
    Consent,
    TheoIntro, 
    IntroScreen,
    parentToggle,
    subtitleToggle, 
    audioToggle,  
    StartGame,
    machineOpen,
    machineLoaded,
    MenuScreen, 
    None,

    }

    void Awake()
    {
        participantId = PlayerPrefs.GetString("ID");
    }

    // Start is called before the first frame update
    void Start()
    {
        machineClick = FindObjectOfType<MachineOnClick>();
        id = FindObjectOfType<IDScript>();    
        animator = GetComponent<Animator>();
        aSources = GetComponents<AudioSource>();  
        button.SetActive(false);
        string subtitleOn = PrefsKeys.Keys.Subtitle.ToString();
        PlayerPrefs.SetInt(subtitleOn, 1);
        string audioOn = PrefsKeys.Keys.SpokenAudio.ToString();
        PlayerPrefs.SetInt(audioOn, 1);

        string prefsKey = PrefsKeys.Keys.IntroLoaded.ToString();
        introLoaded = PlayerPrefs.GetInt(prefsKey, 0) == 1;
        stateToLoad = State.titleScreen;
    }

    // Update is called once per frame
    void Update()
    {
    time += Time.deltaTime;


    AudioBundle[] audioInfo;

        switch (stateToLoad)
    {
        case State.titleScreen:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0f),};
        titlescreen.SetActive(true);
        StartCoroutine(playAudio(audioInfo));

        finishStateLoad();
        break;

        case State.Consent:
        consent.SetActive(true);
        titlescreen.SetActive(false);
        finishStateLoad();
        break;

        case State.TheoIntro:

        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 0f),};
        consent.SetActive(false);
        intro.SetActive(true);
        StartCoroutine(playAudio(audioInfo));

        finishStateLoad();
        break;

        case State.IntroScreen:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 0f),};

        introscreen.SetActive(true);
        intro.SetActive(false);
        StartCoroutine(playAudio(audioInfo));

        finishStateLoad();

        break;

        case State.parentToggle:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay: 0f),};
        ParentToggle.SetActive(true);
        StartCoroutine(playAudio(audioInfo));

        finishStateLoad();

        break;

        case State.subtitleToggle:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 4, delay: 0f),};
        SubtitleToggle.SetActive(true);
        StartCoroutine(playAudio(audioInfo));

        finishStateLoad();

        break;

        case State.audioToggle:
        introscreen.SetActive(true);
        ParentToggle.SetActive(true);
        SubtitleToggle.SetActive(true);
        AudioToggle.SetActive(true);
        button.SetActive(true);
        titlescreen.SetActive(false);
        consent.SetActive(false);
        finishStateLoad();

        break;


        case State.machineOpen:
        introscreen.SetActive(false);
        machine.SetActive(true);
        
        finishStateLoad();

        break;

    }

    }

    public void SkiptoMM()
    {
        stateToLoad = State.machineOpen;
        first = false;
    }

    public void StartGame()
    {
        stateToLoad = State.machineOpen;
        text.SetActive(false);
        string introLoaded = PrefsKeys.Keys.IntroLoaded.ToString();
        PlayerPrefs.SetInt(introLoaded,0);
    }

    public void machineready()
    {
        wasClicked=false;
        console.SetActive(true);
        Debug.Log("console");
        machine.SetActive(false);
        button.SetActive(true);

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
    
    public void moveToNextState()
    {
        stateToLoad = currentState.Next();
    }

    void finishStateLoad()
    {
        currentState = stateToLoad;
        stateToLoad = State.None;
    }

    string getFolderPath()
    {
        string dataPath;
        
#if   UNITY_EDITOR
        dataPath = Application.dataPath + "/CSV";
#elif UNITY_ANDROID
        dataPath = Application.persistentDataPath;
#elif UNITY_IPHONE
        dataPath = Application.persistentDataPath;
#else
        dataPath = Application.dataPath;
#endif
        return $"{dataPath}/{participantId}/";
    }
}