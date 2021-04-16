using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Rating_Manager : MonoBehaviour
{
    public Stimulus[] stickerCat;
    public GameObject pos;
    public GameObject neutral;
    public GameObject neg;

    public GameEvent trialEnd;
    public int index;


    public void OnStartTrial()
    {
        stickerCat[index].SetActive(true);
        pos.SetActive(true);
        neutral.SetActive(true);
        neg.SetActive(true);

    }


    public void OnResponseWindowEnd()
    {
        stickerCat[index].SetActive(false);
        index++;
        trialEnd.Raise();
    }
}
