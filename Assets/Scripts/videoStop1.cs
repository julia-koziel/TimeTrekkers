using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoStop1 : MonoBehaviour
{
    float time = 0;
    float stop;
    public float speed = 1.0f;
    UnityEngine.Video.VideoPlayer videoPlayer;
    //public GameObject display;

    private void Start()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playbackSpeed = speed;
        stop = (float) videoPlayer.clip.length;
    }
    void Update()
    {
        //display.SetActive(true);
        time += Time.deltaTime;
        // print(time);
        if( time > stop)
        {
            time = 0;
            print("off");
            gameObject.SetActive(false);
        }
    }
}
