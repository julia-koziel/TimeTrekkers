using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorisationClick : MonoBehaviour
{       
    public int dinoType;
    private CategorisationPretest logic;
    public GameObject[] stimuli;
    public GameObject tick;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<CategorisationPretest>();  
        sound = GetComponent<AudioSource>();
        tick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked");

        logic.correctclicked();
        tick.SetActive(true);
        sound.Play();
        StartCoroutine(clickdelay());

    }

      private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1.9f); 
        tick.SetActive(false);
    }

}
