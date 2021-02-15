using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCorrectSocial : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject person;
    public GameObject milk;
    Renderer rend;
    private GameLogicRewardSocial gameLogic;
    private float time;
    private float audioTime = 2.5f;
    private bool spokenAudioEnabled;
    // private AudioSource childReaction;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    void OnEnable()
    {
        rend.enabled = true;
    }
    private void Start()
    {
        person
.SetActive(false);
        gameLogic = FindObjectOfType<GameLogicRewardSocial>();

//         string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
//         spokenAudioEnabled = PlayerPrefs.GetInt(prefsKey, 0) == 1;

//         string langKey = PrefsKeys.Keys.Language.ToString();
//         int language = PlayerPrefs.GetInt(langKey, (int)PrefsKeys.Language.GBR);
//         childReaction = person
// .GetComponents<AudioSource>()[language];
    }

    private void Update()
    {
        if (!gameLogic.j)
        {
            time = gameLogic.setTime();
            
            if (time > audioTime)
            {
                gameLogic.resetTime();
                gameLogic.startcorrecthouse();
                gameLogic.reactivateAll();
            }
        }

    }
    public void OnMouseDown()
    {
        if (gameLogic.j){
            gameLogic.logReactionTime();
            rend.enabled = false;
            person.SetActive(true);
            // childReaction.Play();
            
            milk.SetActive(true);
            milk.transform.position = new Vector3(transform.position.x + 0.75f,
                                                  transform.position.y - 1.75f,
                                                  transform.position.z);

            gameLogic.correcthouse();
            gameLogic.pointer2();
            
            gameLogic.resetTime();
        }

    }
    


}
