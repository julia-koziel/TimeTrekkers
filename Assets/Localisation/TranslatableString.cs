using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="Custom/Localisation/TranslatableString")]
public class TranslatableString : ScriptableObject
{
    // TODO write test to check for missing translations
    public LanguageManager languageManager;
    public string[] translatedStrings;
    public string TranslatedString { get => translatedStrings[languageManager.languageCodes.IndexOf(languageManager.currentLanguage)]; }
    public string DefaultString { get => translatedStrings[languageManager.languageCodes.IndexOf(languageManager.defaultLanguage)]; }
    
    public override string ToString() 
    {
        var str = translatedStrings[languageManager.languageCodes.IndexOf(languageManager.defaultLanguage)];
        str = str.Replace('\n',' ');
        str = str.Replace("\"", string.Empty);
        str = str.Replace("'", "");
        return str;
    } 
}