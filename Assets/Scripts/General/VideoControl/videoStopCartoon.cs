using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoStopCartoon : MonoBehaviour
{
    float time = 0;
    public GameObject emptyStill;

    public float stop = 7;
    public float speed = 1.0f;
    private bool hasStarted = false;
    private VideoPlayer videoPlayer;
    private void Start()
    {
        videoPlayer = FindObjectOfType<UnityEngine.Video.VideoPlayer>();
        // videoPlayer.playbackSpeed = speed;
        emptyStill.SetActive(true);
        //stop = (float) videoPlayer.clip.length;
    }
    void Update()
    {
        time += Time.deltaTime;
        // print(time);
        // if( time > stop)
        // {
        //     time = 0;
        //     gameObject.SetActive(false);
        // }
        if (!hasStarted)
        {
            hasStarted = videoPlayer.isPlaying;
        }
        else
        {
            if (!videoPlayer.isPlaying)
            {
                emptyStill.SetActive(false);
                hasStarted = false;
                gameObject.SetActive(false);
            }
        }
    }
}
