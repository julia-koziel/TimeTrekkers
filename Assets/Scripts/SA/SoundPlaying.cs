using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlaying : MonoBehaviour
{
    public GameObject[] sounds;

    public int currentsound;
    private float time;
    public bool correct;
    private bool p = true;
    private int soundtrial;


    // Start is called before the first frame update
    void Start()
    {
    
    foreach (GameObject sound in sounds)
    {
        sound.SetActive(false);
    }
    }

    // Update is called once per frame
    void Update()
    {
        if (p)
        {
            time += Time.deltaTime;
        }
        if (time>3.5f && time <4)
        {
          foreach (GameObject sound in sounds)
          sound.SetActive(false); 
        }

        if (time >4) 
        {
            playSound();
        }
    }

    public void playSound()
    {
    time =0;
    currentsound = Random.Range(0,4);
    sounds[currentsound].SetActive(true);
    soundtrial+=1;
    }

    public void setCorrect()
    {
        if(currentsound==0)
        {
            correct=true;
        }
        
        else
        {
            correct = false;
        }
        
    }
}
