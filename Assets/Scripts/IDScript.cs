using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDScript : MonoBehaviour
{
    public GameObject placeholder;
    public Text inputText;
    public StringVariable participantID;
    bool noId = false;
    void Start()
    {
        if (!string.IsNullOrEmpty(participantID.Value) && participantID.Value != "test")
        {
            placeholder.SetActive(false);
            inputText.SetActive(true);
            inputText.text = participantID.Value;
        }
    }

    public void SaveToPlayerPrefs(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            if (noId)
            {
                participantID.SaveValue("test");
                print("test");
            }
            id = participantID.Value;
        }
        else participantID.SaveValue(id);
        PlayerPrefs.SetString("ID", id);
    }
}