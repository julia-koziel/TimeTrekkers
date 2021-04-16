using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicSpaceDemo : MonoBehaviour
{
    public GameObject starsContainer;
    public GameObject planetsContainer;
    public GameObject canvas;
    private Animator starsAnimator;
    public GameObject spaceshipsContainer;
    private SnowballParticleSystem spaceships;
    public GameObject trials;
    public GameObject startButton;
    public GameObject demoButton;
    public GameObject finishButton;
    private bool buttonsClicked = false;
    public GameObject planetLeft;
    public GameObject planetRight;
    public GameObject alien;
    public GameObject hand;
    public GameObject MM;
    AudioSource[] aSources;
    private bool spokenAudioEnabled;
    private HandBehaviourSpace handBehaviour;
    private enum State {
        None,
        StarsIntro,
        StarsZoom,
        PlanetIntro,
        SpaceshipIntro,
        SpaceshipQuestion,
        SpaceshipLeftQuestion, SpaceshipLeftClick,
        ITI,
        SpaceshipRightQuestion, SpaceshipRightClick,
        Buttons, ButtonsStartAudio, Trials, End, EndButton
    };
    private State stateToLoad;
    private State currentState;
    // Start is called before the first frame update
    void Start()
    {
        handBehaviour = hand.GetComponent<HandBehaviourSpace>();
        spaceships = spaceshipsContainer.GetComponent<SnowballParticleSystem>();
        aSources = GetComponents<AudioSource>();
        starsAnimator = canvas.GetComponent<Animator>();
        spaceships.setParams(dir: Vector3.left, coh: 1);
        // spaceships.switchOn(false);
        hand.SetActive(false);

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        spokenAudioEnabled = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        stateToLoad = State.StarsIntro;
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;

        switch (stateToLoad)
        {
            case State.StarsIntro:
                planetRight.SetActive(false);
                planetLeft.SetActive(false);
                starsContainer.SetActive(true);

                audioInfo = new AudioBundle[]{
                    new AudioBundle("It’s night-time and Pip is looking at the stars with his telescope.",
                                    index: 0, delay: 0)
                };
                
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.StarsZoom:
                finishStateLoad();
                planetsContainer.SetActive(true);
                starsAnimator.SetBool("Zoom", true);
                break;

            case State.PlanetIntro:
                planetRight.SetActive(true);
                planetLeft.SetActive(true);
                planetsContainer.SetActive(false);

                audioInfo = new AudioBundle[]{
                    new AudioBundle("There are two planets!",
                                    index: 1, delay: 0)
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.SpaceshipIntro:
                // spaceships.switchOn(true);

                audioInfo = new AudioBundle[]{
                    new AudioBundle("And look! there are thousands and thousands of spaceships flying through the sky!!",
                                    index: 2, delay: 0)
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.SpaceshipQuestion:
                hand.SetActive(true);
                handBehaviour.reset();

                audioInfo = new AudioBundle[]{
                    new AudioBundle("Where are most of the spaceships flying to?",
                                    index: 3, delay: 0)
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.SpaceshipLeftQuestion:

                audioInfo = new AudioBundle[]{
                    new AudioBundle("Are most of the spaceships flying to this planet?",
                                    index: 4, delay: 0.5f)
                };

                handBehaviour.move(HandBehaviourSpace.State.PointLeftPlanet);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.SpaceshipLeftClick:

                audioInfo = new AudioBundle[]{
                    new AudioBundle("Okay, then press this planet.",
                                    index: 5, delay: 0)
                };

                handBehaviour.move(HandBehaviourSpace.State.ClickLeftPlanet);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.ITI:
                // spaceships.switchOn(false);
                spaceships.setParams(dir: Vector3.right, coh: 0.8f);
                finishStateLoad();
                break;

            case State.SpaceshipRightQuestion:
                alien.SetActive(false);
                // spaceships.switchOn(true);
                Debug.Log("setting false");
                spaceships.setParams(dir: Vector3.right, coh: 0.8f);

                audioInfo = new AudioBundle[]{
                    new AudioBundle("Or are most of the spaceships flying to this planet?",
                                    index: 6, delay: 0)
                };

                handBehaviour.move(HandBehaviourSpace.State.PointRightPlanet);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.SpaceshipRightClick:
                
                audioInfo = new AudioBundle[]{
                    new AudioBundle("Okay, then press this planet.",
                                    index: 7, delay: 0)
                };
                
                handBehaviour.move(HandBehaviourSpace.State.ClickRightPlanet);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.Buttons:
                audioInfo = new AudioBundle[]{
                    new AudioBundle("Now you have a go.",
                                    index: 8, delay: 0)
                };
                
                planetLeft.SetActive(false);
                planetRight.SetActive(false);
                // spaceships.switchOn(false);
                spaceships.setParams(dir: Vector3.left, coh: 1);
                hand.SetActive(false);
                alien.SetActive(false);

                buttonsClicked = false;
                startButton.GetComponent<Button>().interactable = false;
                demoButton.GetComponent<Button>().interactable = false;
                startButton.SetActive(true);
                demoButton.SetActive(true);

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.ButtonsStartAudio:
                audioInfo = new AudioBundle[]{
                    new AudioBundle("Touch the planet where most of the spaceships are flying to.",
                                    index: 9, delay: 0)
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.Trials:
                demoButton.SetActive(false);
                startButton.SetActive(false);
                trials.SetActive(true);
                finishStateLoad();
                moveToNextState();
                gameObject.SetActive(false);
                break;

            case State.End:
                starsContainer.SetActive(true);
                planetsContainer.SetActive(false);
                starsAnimator.SetTrigger("Reset");
                audioInfo = new AudioBundle[]{
                    new AudioBundle("Great, thanks for playing.",
                                    index: 10, delay: 0)
                };
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.EndButton:
                finishButton.SetActive(true);
                finishStateLoad();
                break;

            case State.None:
                break;
        }
    }

    public void moveToNextState()
    {
        // Function may not be needed depending on audio files
        stateToLoad = currentState.Next();
        if (stateToLoad == State.PlanetIntro)
        {
            starsContainer.SetActive(false);
        }
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
            if (spokenAudioEnabled) { currentAudio.Play(); }
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        // Handle end of audio
        if (currentState == State.Buttons)
        {
            startButton.GetComponent<Button>().interactable = true;
            demoButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            moveToNextState();
        }
    }

    public void restartDemo()
    {
        if (!buttonsClicked)
        {
            buttonsClicked = true;
            demoButton.SetActive(false);
            startButton.SetActive(false);
            
            stateToLoad = State.StarsIntro;

            planetsContainer.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            starsContainer.SetActive(true);
            
            starsAnimator.SetTrigger("Reset");
        }
        
    }

    public void startTrials()
    {
        if (!buttonsClicked)
        {
            buttonsClicked = true;
            startButton.GetComponent<Button>().interactable = false;
            demoButton.GetComponent<Button>().interactable = false;
            stateToLoad = State.ButtonsStartAudio;
        }
    }

    public void goToMM()
    {
        MM.SetActive(true);
    }

    public void onEndZoom()
    {
        if (currentState == State.StarsZoom)
        {
            moveToNextState();
        }
    }

    public void skip()
    {
        if ((int) currentState < (int) State.Buttons)
        {
            handBehaviour.reset();
            starsContainer.SetActive(false);
            planetsContainer.SetActive(false);
            starsAnimator.SetBool("Zoom", false);
            starsAnimator.Play("Zoomed");

            foreach (AudioSource audioSource in aSources)
            {
                if (audioSource.isPlaying) { audioSource.Stop(); }
            }
            StopAllCoroutines();

            stateToLoad = State.Buttons;
        }
    }
}
