using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextClickController : MonoBehaviour, IPointerClickHandler
{
    private Camera UICamera;
    private Text textComp;

    private void Start()
    {
        textComp = GetComponent<Text>();
        UICamera = Camera.main;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string clickedWord = UITextUtilities.FindIntersectingWord(textComp, eventData.position, UICamera);
        Debug.Log("Clicked Word: " + clickedWord);
        if (!string.IsNullOrEmpty(clickedWord) && UITextUtilities.hasLinkText(clickedWord))
        {
            string actualUrl = UITextUtilities.RemoveHtmlLikeTags(clickedWord);
            Debug.Log("Opening link: " + actualUrl);
            Application.OpenURL(actualUrl);
        }
    }
}
