using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Assertions;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioTranslator))]
public class VideoAudioPlayer : MonoBehaviour 
{
    public TranslatableAudioClip[] audioClips;
    public int[] frames;
    bool[] played;
    VideoPlayer videoPlayer;
    AudioTranslator audioTranslator;

    void Awake()
    {
        Assert.AreEqual(audioClips.Length, frames.Length, "You must have a frame index for each audioclip");
        videoPlayer = GetComponent<VideoPlayer>();
        audioTranslator = GetComponent<AudioTranslator>();
        played = new bool[audioClips.Length];
    }

    void OnEnable() => played.ForEach(p => p = false);

    void Update()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (!played[i] && videoPlayer.frame > frames[i])
            {
                audioTranslator.Play(audioClips[i]);
                played[i] = true;
            }
        }
    }

    public void Reset()
    {
        audioTranslator.audioSource.Stop();
    }
}