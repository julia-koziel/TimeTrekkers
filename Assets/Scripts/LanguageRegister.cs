using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LanguageRegister : MonoBehaviour
{
    public GameObject[] buttons;
    public Toggle spokenAudioToggle;
    public LanguageManager languageManager;
    public float duration;
    private bool isFanned = false;
    private List<TextTranslator> textTranslators = new List<TextTranslator>();
    TranslatableText[] tts;
    // Start is called before the first frame update
    void Start()
    {
        tts = FindObjectsOfType<TranslatableText>();
        string languageKey = PrefsKeys.Keys.Language.ToString();
        int language = languageManager.languageCodes.IndexOf(languageManager.currentLanguage);
        print(language);
        buttons[language].transform.SetAsLastSibling();
        // spokenAudioToggle.isOn = language == (int)PrefsKeys.Language.GBR;
    }

    Action<float> Move(GameObject go, int position)
    {
        var rt = go.GetComponent<RectTransform>();
        return dt => {
            var x = position * dt / duration;
            
            rt.anchorMin = new Vector2(0.5f - x, 0.5f);
            rt.anchorMax = new Vector2(0.5f - x, 0.5f);
        };
    }

    public void onLangClick(int language)
    {
        if (isFanned)
        {
            print(language);
            for (int i = 0; i < language; i++)
            {
                buttons[i].transform.SetAsLastSibling();
            }
            for (int i = buttons.Length - 1; i >= language ; i--)
            {
                buttons[i].transform.SetAsLastSibling();
            }

            string languageKey = PrefsKeys.Keys.Language.ToString();
            PlayerPrefs.SetInt(languageKey, language);
            languageManager.SaveCurrentLanguage(language);
            tts.ForEach(tt => tt.OnLanguageChange());
            updateTextTranslators();

            spokenAudioToggle.isOn = language == (int)PrefsKeys.Language.GBR;

            this.BuildMovement()
                .First(duration, false, true, buttons.Select((go, i) => Move(go, buttons.Length-1-i)).ToArray())
                .Then(() => isFanned = false)
                .Start();
        }
        else
        {
            this.BuildMovement()
                .First(duration, false, false, buttons.Select((go, i) => Move(go, buttons.Length-1-i)).ToArray())
                .Then(() => isFanned = true)
                .Start();
        }
    }

    public void registerTextTranslator(TextTranslator translator)
    {
        textTranslators.Add(translator);
    }

    private void updateTextTranslators()
    {
        foreach (TextTranslator translator in textTranslators)
        {
            translator.updateTranslation();
        }
    }
}
