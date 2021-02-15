using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioscript : MonoBehaviour
{
    // Start is called before the first frame update
    public class AudioExample : MonoBehaviour
{
    public float pitchValue = 1.0f;

    private AudioSource audioSource;
    private float low = 0.75f;
    private float high = 1.25f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    void OnGUI()
    {
        pitchValue = GUI.HorizontalSlider(new Rect(25, 75, 100, 30), pitchValue, low, high);
        audioSource.pitch = pitchValue;
    }
}
}
