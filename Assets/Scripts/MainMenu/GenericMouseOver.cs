using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMouseOver : MonoBehaviour
{

    public GameObject button1;
    public GameObject button2;
    public GameObject text;


    private void OnMouseOver()
    {
        button1.SetActive(true);
        button2.SetActive(true);
        text.SetActive(false);
    }

    private void OnMouseExit()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        text.SetActive(true);
    }
}
