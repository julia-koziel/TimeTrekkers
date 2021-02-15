using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundcontrol : MonoBehaviour
{
    private Toggle toggle;
    private bool isChecked;
    public AudioSource click;
    // Start is called before the first frame update
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    void OnEnable()
    {
        string spokenAudioKey = PrefsKeys.Keys.SpokenAudio.ToString();
        int isOnInt = PlayerPrefs.GetInt(spokenAudioKey, 1);
        isChecked = isOnInt == 1;
        toggle.isOn = isChecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleSpokenAudio(bool isOn)
    {
        if (isOn) { click.Play(); }

        string spokenAudioKey = PrefsKeys.Keys.SpokenAudio.ToString();
        int isOnInt = isOn ? 1 : 0;
        PlayerPrefs.SetInt(spokenAudioKey, isOnInt);
        Debug.Log("" + PlayerPrefs.GetInt(spokenAudioKey, 1));
    }
    
}
