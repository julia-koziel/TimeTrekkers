using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[AddComponentMenu("Audio/AudioTranslator")]
[RequireComponent(typeof(AudioSource))]
public class AudioTranslator : MonoBehaviour
{
    public TranslatableAudioClip audioClip;
    public AudioSource audioSource;
    public BoolVariable spokenAudioEnabled;
    public bool playOnAwake = false;
    public void setClip(TranslatableAudioClip translatableAudioClip)
    {
        audioClip = translatableAudioClip;
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClip != null) audioSource.clip = audioClip.TranslatedAudioClip;
        if (playOnAwake && !shouldMute(audioClip)) audioSource.Play();
    }

# if UNITY_EDITOR
    void OnValidate() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        if (audioClip != null) audioSource.clip = audioClip.TranslatedAudioClip;

        if (spokenAudioEnabled == null)
        {
            var guids = AssetDatabase.FindAssets("SpokenAudioEnabled t:BoolVariable");
            if (guids.Length == 1)
            {
                spokenAudioEnabled = (BoolVariable)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(BoolVariable));
            }
            else Debug.LogWarning("You should have one and only one bool variable called \"SpokenAudioEnabled\"");
        }
    }
# endif

    public void Play(TranslatableAudioClip clip = null)
    {
        clip = clip ?? audioClip;
        if (!shouldMute(clip))
        {
            audioSource.clip = clip.TranslatedAudioClip;
            audioSource.Play();
        }
    }

    public float clipLength { 
        get => getLength(audioClip);
    }

    public float getLength(TranslatableAudioClip clip)
    {
        if (clip == null) return 0;

        return shouldMute(clip) ? 0 : clip.length;
    }

    bool shouldMute(TranslatableAudioClip clip) => clip.isMuteable && !spokenAudioEnabled;
}