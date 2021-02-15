using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeTypeControl : MonoBehaviour
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
        string parentPractice = PrefsKeys.Keys.PracticeType.ToString();
        int isOnInt = PlayerPrefs.GetInt(parentPractice, 1);
        isChecked = isOnInt == 1;
        toggle.isOn = isChecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePracticeType(bool isOn)
    {
        click.Play();

        string parentPractice = PrefsKeys.Keys.PracticeType.ToString();
        int isOnInt = isOn ? 1 : 0;
        PlayerPrefs.SetInt(parentPractice, isOnInt);
        Debug.Log("" + PlayerPrefs.GetInt(parentPractice, 1));

    }

    
}