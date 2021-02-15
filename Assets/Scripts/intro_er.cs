using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_er : MonoBehaviour
{

    public GameObject gamelogic;

    public GameObject Pip;
    public GameObject Emma;
    public GameObject Selina;
    public GameObject Tiago;

    public GameObject[] texts;

    int clicks;

    void Start()
    {

        Pip.SetActive(true);
        foreach (GameObject text in texts) { text.SetActive(false); }
        texts[0].SetActive(true);
    }

    private void OnMouseDown()
    {
        switch (clicks)
        {
            case 0:
                Pip.SetActive(false);
                Selina.SetActive(true);
                texts[0].SetActive(false);
                texts[1].SetActive(true);
                clicks += 1;
                break;
            case 1:
                Emma.SetActive(true);
                Selina.SetActive(false);
                texts[1].SetActive(false);
                texts[2].SetActive(true);
                clicks += 1;
                break;
            case 2:
                Tiago.SetActive(true);
                Emma.SetActive(false);
                texts[2].SetActive(false);
                texts[3].SetActive(true);
                clicks += 1;
                break;
            case 3:
                Tiago.SetActive(false);
                gamelogic.SetActive(true);
                texts[3].SetActive(false);
                clicks += 1;
                break;
        }
    }
}
