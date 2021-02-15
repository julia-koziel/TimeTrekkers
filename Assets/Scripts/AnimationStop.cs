using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStop : MonoBehaviour
{
    float time = 0;
    float stop;
    public float speed = 1.0f;
    Animation anim;
    //public GameObject display;

    private void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play();
    }
    void Update()
    {
        //display.SetActive(true);
        time += Time.deltaTime;
        // print(time);
        if(anim.isPlaying)
        {
            time = 0;
            print("off");
            gameObject.SetActive(false);
        }
    }
}
