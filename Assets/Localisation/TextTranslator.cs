using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTranslator : MonoBehaviour
{
    private Text textComponent;
    private string englishText;
    private bool isMainMenu = false;
    private string mainMenuSceneName = "MainMenu_w_versions";
    // Start is called before the first frame update
    void Awake()
    {
        textComponent = GetComponent<Text>();
        englishText = textComponent.text;
        textComponent.text = Languages.translate(englishText);
        isMainMenu = SceneManager.GetActiveScene().name == mainMenuSceneName;
    }

    void Start()
    {
        if (isMainMenu)
        {
            LanguageRegister languageRegister = FindObjectOfType<LanguageRegister>();
            if (languageRegister)
            {
                languageRegister.registerTextTranslator(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTranslation()
    {
        textComponent.text = Languages.translate(englishText);
    }
}
