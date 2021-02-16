using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyEnd : MonoBehaviour
{
    public GameObject mm;
    public GameObject finishButton;
    private bool audioStarted = false;
    private AudioSource endAudio;

    // Start is called before the first frame update
    void Start()
    {
        endAudio = GetComponent<AudioSource>();
        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioStarted)
        {      
            StartCoroutine(showFinishButton());
        }
    }

    private IEnumerator showFinishButton()
    {
        yield return new WaitForSeconds(endAudio.clip.length);
        finishButton.SetActive(true);
    }

    public void goToMainMenu()
    {
        mm.SetActive(true);
    }
}
