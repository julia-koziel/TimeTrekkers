using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
public class IntroAnimation : MonoBehaviour
{
    public GameObject background;
    public GameEvent stageEnd;
    RawImage emptyStill;
    private bool hasStarted = false;
    private VideoPlayer videoPlayer;
    long frozenFrame = 0;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if (!hasStarted)
        {
            hasStarted = videoPlayer.isPlaying;
        }
        else
        {
            var frame = videoPlayer.frame;

            if (!videoPlayer.isPlaying || frame > (long)videoPlayer.frameCount - 2)
            {
                stageEnd.Raise();
            }
            else if (frame > (long)videoPlayer.frameCount - 20)
            {
                if (frame == frozenFrame) stageEnd.Raise();
                else frozenFrame = frame;
            }
        }
        
        // if (videoPlayer.frame.IsBetweenEE(1, 10)) background.SetActive(false);
    }


    public void Reset()
    {
        hasStarted = false;
        background.SetActive(true);
        gameObject.SetActive(false);
        frozenFrame = 0;
    }
}

