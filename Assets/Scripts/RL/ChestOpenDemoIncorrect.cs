using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenDemoIncorrect : MonoBehaviour
{
    public GameObject openChest;
    public GameObject buttonincorrect;
    public GameObject buttoncorrect;
    public GameObject description;
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
            openChest.SetActive(true);
            buttonincorrect.SetActive(true);
            buttoncorrect.SetActive(false);
            description.SetActive(false);
            reactivateall();
    }

    public void reactivateall()
    {
        openChest.SetActive(false);
    }
}
