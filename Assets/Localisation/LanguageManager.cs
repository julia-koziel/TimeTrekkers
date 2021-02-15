using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="Custom/Localisation/Language Manager")]
public class LanguageManager : ScriptableObject
{
    public StringVariable[] languageCodes;
    [Space(10)]
    public StringVariable currentLanguage;
    public StringVariable defaultLanguage;
    void OnEnable() 
    {
        var lang = PlayerPrefs.GetInt(GetInstanceID().ToString(), languageCodes.IndexOf(defaultLanguage));
        currentLanguage = languageCodes[lang];
    }
    public void SaveCurrentLanguage(int? lang = null)
    {
        var language = lang ?? languageCodes.IndexOf(currentLanguage);
        currentLanguage = languageCodes[language];
        PlayerPrefs.SetInt(GetInstanceID().ToString(), language);
    }
# if UNITY_EDITOR
    void OnValidate()
    {
        SaveCurrentLanguage();
    }
# endif
}