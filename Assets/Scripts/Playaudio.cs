using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playaudio : MonoBehaviour
{

    public AudioSource som;

    void Start()
    {
        som.Play();
    }


}