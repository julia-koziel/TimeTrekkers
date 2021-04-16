using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newclicksbees : MonoBehaviour
{
    public GameObject logicobject;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject practicetrial;

    int block = 0;

    void Start()
    {

        text1.SetActive(true);
    }

    private void Update()
    {
        logicobject.SetActive(false);
    }

    private void OnMouseDown()
    {
        print("Clicked");
        switch (block)
        {
            case 0:
                text1.SetActive(false);
                practicetrial.SetActive(true);
                text2.SetActive(true);
                block += 1;
                gameObject.SetActive(false);
                break;
            case 1:
                text2.SetActive(false);
                logicobject.SetActive(true);
                block += 1;
                gameObject.SetActive(false);
                break;
            case 2:
                practicetrial.SetActive(false);
                text3.SetActive(true);
                logicobject.SetActive(true);
                block += 1;
                gameObject.SetActive(false);
                break;
            case 3:
                text3.SetActive(false);
                text4.SetActive(true);
                logicobject.SetActive(true);
                block += 1;
                gameObject.SetActive(false);
                break;
            case 4:
                logicobject.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }
}
