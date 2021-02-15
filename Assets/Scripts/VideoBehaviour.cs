using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoBehaviour : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private MainMenuLogic gameLogic;
    public GameObject frame;

    public GameObject frame2;
    public GameObject frame3;


    public bool p = false;
    public bool d = false;
    public bool t = false;
    public bool q = false;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<MainMenuLogic>();
        videoPlayer = GetComponent<VideoPlayer>();
        Debug.Log(videoPlayer.clip.frameCount);
        p=false;
    }


    // Update is called once per frame
    void Update()
    {

        if (p)
        {
        }

        if (d)
        {
            videoPlayer.Pause();
            frame2.SetActive(true);
        }

        if (t)
        {
            videoPlayer.Pause();
            frame.SetActive(true);
        }

        if(q)
        {
            videoPlayer.Pause();
            frame3.SetActive(true);
        }

    }

    public void PirateVideo()
    {
       p =true;
    }

    public void DinoVideo()
    {
        d = true;
    }

    public void TreasureVideo()
    {
        t = true;
    }

    public void QueensVideo()
    {
        q = true;
    }


}
