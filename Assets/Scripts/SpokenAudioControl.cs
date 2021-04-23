using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpokenAudioControl : MonoBehaviour
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
        string audioKey = PrefsKeys.Keys.SpokenAudio.ToString();
        int isOnInt = PlayerPrefs.GetInt(audioKey, 1);
        isChecked = isOnInt == 1;
        toggle.isOn = isChecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleAudio (bool isOn)
    {
        click.Play();

        string audioKey = PrefsKeys.Keys.SpokenAudio.ToString();
        int isOnInt = isOn ? 1 : 0;
        PlayerPrefs.SetInt(audioKey, isOnInt);
        Debug.Log("" + PlayerPrefs.GetInt(audioKey, 1));
    }
    
}
