using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    public GameObject textHeaderTop;
    public GameObject textHeaderBottom;
    public Button backButton;
    public Button continueButton;
    public Button customContinueButton;
    [HideInInspector]
    public string topText, bottomText, backText, customText, continueText;
    public bool waitForParent;
    public bool cancelWaitForParent = false;
    AudioSource _tone;
    AudioSource tone {
        get {
            if (_tone == null) _tone = GetComponent<AudioSource>();
            return _tone;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showElement(GameObject element, string text)
    {
        element.SetActive(true);
        element.GetComponentInChildren<Text>().text = text;
    }

    void showElement(Button element, string text)
    {
        showElement(element.gameObject, text);
    }

    public void showUI() 
    {
        if (!string.IsNullOrEmpty(topText)) showElement(textHeaderTop, topText);
        if (!string.IsNullOrEmpty(bottomText)) showElement(textHeaderBottom, bottomText);

        refreshHeaders();

        if (!string.IsNullOrEmpty(backText)) showElement(backButton, backText);
        if (!string.IsNullOrEmpty(continueText)) showElement(continueButton, continueText);
        if (!string.IsNullOrEmpty(customText)) showElement(customContinueButton, customText);

        refreshButtons();

        if (waitForParent && !cancelWaitForParent) alertParent();
        else setButtonsInteractable(true);
    }
    public void showUI(string text,
                       string continueText = null, string backText = null, string customText = null,
                       bool textAtBottom = false, bool waitForParent = true
    ) {
        var header = textAtBottom ? textHeaderBottom : textHeaderTop;
        showElement(header, text);

        Canvas.ForceUpdateCanvases();
        header.GetComponent<VerticalLayoutGroup>().enabled = false;
        header.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false;
        header.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
        header.GetComponent<VerticalLayoutGroup>().enabled = true;

        var buttons = backText != null || continueText != null || customText != null;
        if (buttons) showButtons(continueText, backText, customText, waitForParent);
    }

    public void showButtons(string continueText = null, string backText = null, string customText = null,
                            bool waitForParent = true
    ) {
        if (backText != null) showElement(backButton, backText);
        if (continueText != null) showElement(continueButton, continueText);
        if (customText != null) showElement(customContinueButton, customText);
        
        refreshButtons();

        if (waitForParent) alertParent();
        else setButtonsInteractable(true);
    }

    void refreshHeaders()
    {
        Canvas.ForceUpdateCanvases();
        textHeaderTop.GetComponent<VerticalLayoutGroup>().enabled = false;
        textHeaderTop.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false;
        textHeaderTop.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
        textHeaderTop.GetComponent<VerticalLayoutGroup>().enabled = true;
        textHeaderBottom.GetComponent<VerticalLayoutGroup>().enabled = false;
        textHeaderBottom.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false;
        textHeaderBottom.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
        textHeaderBottom.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    void refreshButtons()
    {
        Canvas.ForceUpdateCanvases();

        backButton.GetComponent<HorizontalLayoutGroup>().enabled = false;
        continueButton.GetComponent<HorizontalLayoutGroup>().enabled = false;
        customContinueButton.GetComponent<HorizontalLayoutGroup>().enabled = false;

        backButton.GetComponent<HorizontalLayoutGroup>().enabled = true;
        continueButton.GetComponent<HorizontalLayoutGroup>().enabled = true;
        customContinueButton.GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    void alertParent()
    {
        tone.Play();
        this.In(2).Call(() => setButtonsInteractable(true));
    }

    void setButtonsInteractable(bool interactable)
    {
        backButton.interactable = interactable;
        continueButton.interactable = interactable;
        customContinueButton.interactable = interactable;
    }

    public void hideUI()
    {
        textHeaderTop.SetActive(false);
        textHeaderBottom.SetActive(false);
        backButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        customContinueButton.gameObject.SetActive(false);
        setButtonsInteractable(false);
        topText = null;
        bottomText = null;
        backText = null;
        customText = null;
        continueText = null;
        cancelWaitForParent = false;
    }
}
