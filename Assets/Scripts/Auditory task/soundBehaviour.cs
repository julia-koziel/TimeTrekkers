using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundBehaviour : MonoBehaviour

{
    public AudioSource sound;
    public AudioSource sound2;
    public static soundBehaviour instance = null;
    private float clipLoudness = 0.95f;

    private GameLogicBird Gamelogic;
    private GameLogicBird trial;
    public float time;
    public float volume;

void Start ()
{
    sound = GetComponent<AudioSource>();
    Gamelogic = FindObjectOfType<GameLogicBird>();
    
    // trial = FindObjectOfType<Gamelogictrials>();

}
void Update ()
{
    time = Gamelogic.setcurrentTime();

    // if (Gamelogictrials.trial > 5)
    // {
    // Debug.Log("hey");
    // }
}

public void PlaySingle (AudioClip clip)
{
    sound.clip = clip;
    sound.Play();
}


public void increasevolume()
{
    if (time > 3.999999f)
    {
    Debug.Log("Increasevolume");
    sound.volume = clipLoudness;
    sound.Play();
    Gamelogic.trial_n ++;
    }
    
}

}
