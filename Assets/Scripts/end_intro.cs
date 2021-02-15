using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_intro : MonoBehaviour
{
    float time = 0;
    UnityEngine.Video.VideoPlayer videoPlayer;
    double stop;
    public float speed = 1.0f;
    public GameObject MM;
    private void Start()
    {
        videoPlayer = FindObjectOfType<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playbackSpeed = speed;
        stop = videoPlayer.clip.length;
        print(stop);

        //stop = (float) videoPlayer.clip.length;
    }
    void Update()
    {
        time += Time.deltaTime;
        // print(time);
        if (time > stop)
        {
            MM.SetActive(true);
            time = 0;
        }
    }
}
