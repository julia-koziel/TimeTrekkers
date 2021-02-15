using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AudioBundle
{
    public int index;
    public float delay;
    public string text;

    public AudioBundle(string text, int index, float delay)
    {
        this.text = text;
        this.index = index;
        this.delay = delay;
    }

    public AudioBundle(int index, float delay)
    {
        this = new AudioBundle("N/A", index, delay);
    }

    public AudioBundle(string text, int index)
    {
        this = new AudioBundle(text, index, 0.5f);
    }

    public AudioBundle(int index)
    {
        this = new AudioBundle("N/A", index, 0.5f);
    }
}
