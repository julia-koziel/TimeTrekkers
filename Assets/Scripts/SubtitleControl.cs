using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleControl : MonoBehaviour
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
        string subtitleKey = PrefsKeys.Keys.Subtitle.ToString();
        int isOnInt = PlayerPrefs.GetInt(subtitleKey, 1);
        isChecked = isOnInt == 1;
        toggle.isOn = isChecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleSubtitles (bool isOn)
    {
        click.Play();

        string subtitleKey = PrefsKeys.Keys.Subtitle.ToString();
        int isOnInt = isOn ? 1 : 0;
        PlayerPrefs.SetInt(subtitleKey, isOnInt);
        Debug.Log("" + PlayerPrefs.GetInt(subtitleKey, 1));
    }
    
}
