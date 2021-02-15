using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenDemo : MonoBehaviour
{
    public GameObject openChest;
    public GameObject buttoncorrect;
    public GameObject buttonincorrect;
    public GameObject descriptionButon;
    public GameObject continuebutton;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        openChest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMouseDown()
    {
            time=0;
            openChest.SetActive(true);
            buttoncorrect.SetActive(true);
            buttonincorrect.SetActive(false);
            continuebutton.SetActive(true);
            descriptionButon.SetActive(false);

        // if (time>1)
        // {
        //     openChest.SetActive(false);
        // }
    }
}
