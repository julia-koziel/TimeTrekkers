using UnityEngine;

[CreateAssetMenu(menuName="Custom/Localisation/TranslatableAudio")]
public class TranslatableAudioClip : ScriptableObject 
{
    public LanguageManager languageManager;
    public AudioClip[] translatedAudioClips;
    public AudioClip TranslatedAudioClip { get => translatedAudioClips[languageManager.languageCodes.IndexOf(languageManager.currentLanguage)]; }
    public bool isMuteable = true;
    public float length { get => TranslatedAudioClip == null ? 0 : TranslatedAudioClip.length; }
}