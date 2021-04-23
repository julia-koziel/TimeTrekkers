using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_level1 : MonoBehaviour
{
    public BoolVariable level1;
    public GameObject  background;
    public GameObject titlecard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        background.SetActive(false);
        titlecard.SetActive(true);
    }

    public void SetLevel1()
    {
        level1.Value=1;
        background.SetActive(false);
        titlecard.SetActive(true);
    }
}
